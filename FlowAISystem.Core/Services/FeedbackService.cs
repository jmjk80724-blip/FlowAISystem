using FlowAISystem.Core.Exceptions;
using FlowAISystem.Core.Services.Interfaces;
using FlowAISystem.Data;
using FlowAISystem.Data.DTOs.Feedback;
using FlowAISystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlowAISystem.Core.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly AppDbContext _context;

        public FeedbackService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<FeedbackResponseDto>> GetAllAsync()
        {
            return await _context.Feedbacks
                .Select(f => new FeedbackResponseDto
                {
                    Id           = f.Id,
                    PredictionId = f.PredictionId,
                    IsCorrect    = f.IsCorrect,
                    Correction   = f.Correction,
                    CreatedAt    = f.CreatedAt
                })
                .ToListAsync();
        }

        public async Task<FeedbackResponseDto> CreateAsync(FeedbackCreateDto dto)
        {
            // ១. Validate Prediction exists
            var prediction = await _context.Predictions
                .FirstOrDefaultAsync(p => p.Id == dto.PredictionId);

            if (prediction == null)
                throw new NotFoundException($"Prediction {dto.PredictionId} not found");

            // ២. Validate មិនធ្លាប់មាន Feedback រួចហើយ (One-to-One)
            var exists = await _context.Feedbacks
                .AnyAsync(f => f.PredictionId == dto.PredictionId);

            if (exists)
                throw new DuplicateException("Feedback already exists for this prediction");

            // ៣. Save Feedback
            var feedback = new Feedback
            {
                PredictionId = dto.PredictionId,
                IsCorrect    = dto.IsCorrect,
                Correction   = dto.Correction
            };

            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            // ៤. Update AIModel Accuracy
            await UpdateModelAccuracy(prediction.AIModelId);

            return new FeedbackResponseDto
            {
                Id           = feedback.Id,
                PredictionId = feedback.PredictionId,
                IsCorrect    = feedback.IsCorrect,
                Correction   = feedback.Correction,
                CreatedAt    = feedback.CreatedAt
            };
        }

        // ── Private Helper ──────────────────────

        private async Task UpdateModelAccuracy(int modelId)
        {
            var totalPredictions = await _context.Predictions
                .CountAsync(p => p.AIModelId == modelId);

            if (totalPredictions == 0)
                return;

            var correctPredictions = await _context.Feedbacks
                .Include(f => f.Prediction)
                .CountAsync(f =>
                    f.IsCorrect &&
                    f.Prediction.AIModelId == modelId);

            var model = await _context.AIModels.FindAsync(modelId);

            if (model != null)
            {
                model.Accuracy = (double)correctPredictions / totalPredictions * 100;
                await _context.SaveChangesAsync();
            }
        }
    }
}