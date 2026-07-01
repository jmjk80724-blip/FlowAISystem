using FlowAISystem.Core.Exceptions;
using FlowAISystem.Core.Services.Interfaces;
using FlowAISystem.Data;
using FlowAISystem.Data.DTOs.Prediction;
using FlowAISystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlowAISystem.Core.Services
{
    public class PredictionService : IPredictionService
    {
        private readonly AppDbContext _context;

        public PredictionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<PredictionResponseDto>> GetAllAsync()
        {
            return await _context.Predictions
                .Include(p => p.Enrollment)
                    .ThenInclude(e => e.Student)
                .Include(p => p.Enrollment)
                    .ThenInclude(e => e.Subject)
                .Include(p => p.AIModel)
                .Select(p => new PredictionResponseDto
                {
                    Id             = p.Id,
                    EnrollmentId   = p.EnrollmentId,
                    StudentName    = p.Enrollment.Student.FullName,
                    SubjectName    = p.Enrollment.Subject.SubjectName,
                    AverageScore   = p.AverageScore,
                    Result         = p.Result,
                    Recommendation = p.Recommendation,
                    AIModelName    = p.AIModel.ModelName,
                    CreatedAt      = p.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<PredictionResponseDto> GetByEnrollmentIdAsync(int enrollmentId)
        {
            var prediction = await _context.Predictions
                .Include(p => p.Enrollment)
                    .ThenInclude(e => e.Student)
                .Include(p => p.Enrollment)
                    .ThenInclude(e => e.Subject)
                .Include(p => p.AIModel)
                .FirstOrDefaultAsync(p => p.EnrollmentId == enrollmentId);

            if (prediction == null)
                throw new NotFoundException($"Prediction for Enrollment {enrollmentId} not found");

            return new PredictionResponseDto
            {
                Id             = prediction.Id,
                EnrollmentId   = prediction.EnrollmentId,
                StudentName    = prediction.Enrollment.Student.FullName,
                SubjectName    = prediction.Enrollment.Subject.SubjectName,
                AverageScore   = prediction.AverageScore,
                Result         = prediction.Result,
                Recommendation = prediction.Recommendation,
                AIModelName    = prediction.AIModel.ModelName,
                CreatedAt      = prediction.CreatedAt
            };
        }

        public async Task<PredictionResponseDto> PredictAsync(PredictionRequestDto dto)
        {
            // ១. Validate Enrollment
            var enrollment = await _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(e => e.Id == dto.EnrollmentId);

            if (enrollment == null)
                throw new NotFoundException($"Enrollment {dto.EnrollmentId} not found");

            // ២. រក Scores ទាំងអស់
            var scores = await _context.Scores
                .Where(s => s.EnrollmentId == dto.EnrollmentId)
                .ToListAsync();

            if (!scores.Any())
                throw new NotFoundException("No scores found for this enrollment");

            // ៣. គណនា Weighted Average
            double average = CalculateWeightedAverage(scores);

            // ៤. Predict Result
            string result         = average >= 50 ? "Pass" : "Fail";
            string recommendation = GetRecommendation(average);

            // ៥. រក AIModel
            var aiModel = await _context.AIModels
                .OrderByDescending(m => m.TrainedAt)
                .FirstOrDefaultAsync();
            

            if (aiModel == null)
                throw new NotFoundException("No AI Model found");
            
            var existing = await _context.Predictions
                .FirstOrDefaultAsync(p => p.EnrollmentId == dto.EnrollmentId);
            if(existing != null)
            {
                _context.Predictions.Remove(existing);
                await _context.SaveChangesAsync();
            }

            // ៦. Save Prediction

            var prediction = new Prediction
            {
                EnrollmentId   = dto.EnrollmentId,
                AIModelId      = aiModel.Id,
                AverageScore   = average,
                Result         = result,
                Recommendation = recommendation
            };

            _context.Predictions.Add(prediction);
            await _context.SaveChangesAsync();

            // ៧. Save PredictionLog
            var log = new PredictionLog
            {
                AIModelId    = aiModel.Id,
                PredictionId = prediction.Id,
                Input        = $"EnrollmentId: {dto.EnrollmentId}",
                Output       = $"Result: {result}, Average: {average}",
                Confidence   = average / 100
            };

            _context.PredictionLogs.Add(log);
            await _context.SaveChangesAsync();

            return new PredictionResponseDto
            {
                Id             = prediction.Id,
                EnrollmentId   = prediction.EnrollmentId,
                StudentName    = enrollment.Student.FullName,
                SubjectName    = enrollment.Subject.SubjectName,
                AverageScore   = Math.Round(average, 2),
                Result         = result,
                Recommendation = recommendation,
                AIModelName    = aiModel.ModelName,
                CreatedAt      = prediction.CreatedAt
            };

        }

        // ── Private Helper Methods ──────────────────────

        private double CalculateWeightedAverage(List<Score> scores)
        {
            double assignment = scores
                .Where(s => s.ScoreType == "Assignment")
                .Select(s => s.Value)
                .DefaultIfEmpty(0)
                .Average() * 0.30;

            double midterm = scores
                .Where(s => s.ScoreType == "Midterm" )
                .Select(s => s.Value)
                .DefaultIfEmpty(0)
                .Average() * 0.30;

            double final = scores
                .Where(s => s.ScoreType == "Final")
                .Select(s => s.Value)
                .DefaultIfEmpty(0)
                .Average() * 0.40;

            return assignment + midterm + final;
        }

        private string GetRecommendation(double average)
        {
            return average switch
            {
                >= 80 => "Excellent! Keep it up!",
                >= 70 => "Good! Try harder!",
                >= 50 => "Pass but need improvement!",
                _     => "Fail! Need more study!"
            };
        }
    }
}