
using FlowAISystem.Core.Exceptions;
using FlowAISystem.Core.Services.Interfaces;
using FlowAISystem.Data;
using FlowAISystem.Data.DTOs.Score;
using FlowAISystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlowAISystem.Core.Services
{
    public class ScoreService : IScoreService
    {
        private readonly AppDbContext _context;

        public ScoreService( AppDbContext context)
        {
            _context = context;
        }

    public async Task<List<ScoreResponseDto>> GetAllAsync()
        {
            return await _context.Scores
            .Include( s => s.Enrollment)
             .ThenInclude( e => e.Student)
            .Include( s => s.Enrollment)
            .ThenInclude(e => e.Subject)
            .Select(s => new ScoreResponseDto
            {
                Id = s.Id,
                EnrollmentId = s.EnrollmentId,
                StudentName = s.Enrollment.Student.FullName,
                SubjectName = s.Enrollment.Subject.SubjectName,
                ScoreType = s.ScoreType,
                Value = s.Value,
                CreatedAt = s.CreatedAt
            })
             .ToListAsync();
        }
    
    public async Task<List<ScoreCreateDto>> GetIdEnrollmentIdAsync ( int enrollmentId)
        {
            var enrollment = await _context.Enrollments.FindAsync(enrollmentId);

            if (enrollment==null)
            throw new NotFiniteNumberException($"Enrollment {enrollmentId} not found");

            return await _context.Scores 
            .Where( s => s.EnrollmentId == enrollmentId)
            .Include ( s => s.Enrollment)
            .ThenInclude( e => e.Student)
            .Include ( s => s.Enrollment)
            .ThenInclude (e => e.Subject)
            .Select (s => new ScoreResponseDto
            {
                Id = s.Id,
                EnrollmentId = s.EnrollmentId,
                StudentName = s.Enrollment.Student.FullName,
                SubjectName = s.Enrollment.Subject.SubjectName,
                ScoreType = s.ScoreType,
                Value = s.Value,
                CreatedAt = s.CreatedAt
            }) 
             .ToListAsync();
        }
    public async Task<ScoreResponseDto> CreateAsync( ScoreCreateDto dto)
        {
            var enrollment = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Subject)
            .FirstOrDefaultAsync (e => e.Id ==  dto.EnrollmentId);

             
            if (enrollment == null) 
            throw  new NotFountException($"Enrollment {dto.EnrollmentId} not found");

            var exists = await _context.Scores
            .AnyAsync( s=> 
               s.EnrollmentId ==dto.EnrollmentId && 
               s.ScoreType ==  dto.ScoreType);

            if(exists) 
            throw new DuplicateException($"{dto.ScoreType}  score already exists");

            var score = new Score
            {
                EnrollmentId = dto.EnrollmentId,
                ScoreType  =dto.ScoreType,
                Value = dto.Value
            };

            _context.Scores.Add(score);
            await _context.SaveChangesAsync();

            return new ScoreResponseDto
            {
                Id = score.Id,
                EnrollmentId = score.EnrollmentId,
                StudentName = enrollment.Student.FullName,
                SubjectName = enrollment.Subject.SubjectName,
                ScoreType = score.ScoreType,
                Value = score.Value,
                CreatedAt = score.CreatedAt
            };
        }
    public async Task UpdateAsync (int id , ScoreUpdateDto dto)
        {
            var score = await _context.Scores.FindAsync(id);

            if(score == null) 
            throw new NotFountException($"Score {id} not found");

            score.Valus = dto.Value;

            await _context.SaveChangesAsync();
        }

    public async Task DeleteAsync(int id)
        {
            var score  = await _context.Scores.FindAsync(id);

            if (score==null)
            throw new NotFountException($"Score {id} not found");

            _context.Scores.Remove(score);
            await _context.SaveChangesAsync();
        }

        public Task<ScoreResponseDto> GetEnrollmentIdAsync(int enrollmentId)
        {
            throw new NotImplementedException();
        }

        public Task<ScoreResponseDto> CreateAysync(ScoreCreateDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsynce(int id)
        {
            throw new NotImplementedException();
        }
    }
}