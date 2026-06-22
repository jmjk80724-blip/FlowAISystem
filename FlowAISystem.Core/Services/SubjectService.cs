
using FlowAISystem.Core.Exceptions;
using FlowAISystem.Core.Services.Interfaces;
using FlowAISystem.Data;
using FlowAISystem.Data.DTOs.Student;
using FlowAISystem.Data.DTOs.Subject;
using FlowAISystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlowAISystem.Core.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly AppDbContext _context;

        public SubjectService(AppDbContext context)
        {
            _context  = context;
        }
        public async Task<List<SubjectResponseDto>> GetAllAsync ()
        {
            return await _context.Subjects
            .Select (s => new SubjectResponseDto
            {
               Id = s.Id,
               SubjectName = s.SubjectName,
               Credits = s.Credits,
               Description = s.Description 
            })
            .ToListAsync();
        }
        public async Task<SubjectResponseDto> GetByIdAsync(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if (subject == null)
            throw new NotFoundException($"Subject {id} Notfound");
            
            return new SubjectResponseDto
            {
                Id = subject.Id,
                SubjectName = subject.SubjectName,
                Credits = subject.Credits,
                Description = subject.Description
            };
        } 
        public async Task<SubjectResponseDto> CreateAsync(SubjectCreateDto dto)
        {
            // Validation duplicate
            var exists = await _context.Subjects
             .AnyAsync( s => s.SubjectName == dto.SubjectName);

             if(exists)
             throw new DuplicateException($"Subject already exists");

             var subject =  new Subject
             {
                 SubjectName = dto.SubjectName,
                 Credits = dto.Credits,
                 Description= dto.Description
             };

             _context.Subjects.Add(subject);
             await _context.SaveChangesAsync();

             return new SubjectResponseDto
             {
                 Id = subject.Id,
                 SubjectName = subject.SubjectName,
                 Credits = subject.Credits,
                 Description = subject.Description
             };
        }
        public async Task UpdateAsync(int id , SubjectUpdateDto dto)
        {
            var subject =  await _context.Subjects.FindAsync(id);

            if(subject == null) 
            throw new NotFoundException($"Subject {id} not found");

            subject.SubjectName = dto.SubjectName ?? subject.SubjectName;
            subject.Credits = dto.Credits !=0 ? dto.Credits : subject.Credits;
            subject.Description = dto.Description ?? subject.Description;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);

            if(subject == null)
             throw new NotFoundException($"Subject {id} not found");

            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
        }
    }
}