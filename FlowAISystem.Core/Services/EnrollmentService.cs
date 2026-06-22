

using System.Xml;
using FlowAISystem.Core.Exceptions;
using FlowAISystem.Core.Services.Interfaces;
using FlowAISystem.Data;
using FlowAISystem.Data.DTOs.Enrollment;
using FlowAISystem.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlowAISystem.Core.Services 
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly AppDbContext _context;

        public EnrollmentService (AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<EnrollmentReponseDto>> GetAllAsync()
        {
            return await _context.Enrollments
            .Include( e => e.Student)
            .Include( e => e.Subject)
            .Select( e => new EnrollmentReponseDto
            {
                Id = e.Id,
                StudentId = e.StudentId,
                StudentName = e.Student.FullName,
                SubjectId = e.SubjectId,
                SubjectName = e.Subject.SubjectName,
                Semester = e.Semester,
                Year = e.Year,
                EnrolledAt = e.EnrolledAt

            })
            .ToListAsync();
        }
        public async Task<EnrollmentReponseDto> GetIdAsync(int id)
        {
            var enrollment = await _context.Enrollments
            .Include(e => e.Student)
            .Include(e => e.Subject) 
            .FirstOrDefaultAsync( e => e.Id == id);

            if(enrollment == null)
            throw new NotFoundException($"Enrollment {id} not found");

            return new EnrollmentReponseDto
            {
                Id         = enrollment.Id,
                StudentId = enrollment.StudentId,
                StudentName = enrollment.Student.FullName,
                SubjectId = enrollment.SubjectId,
                SubjectName = enrollment.Subject.SubjectName,
                Semester = enrollment.Semester,
                Year = enrollment.Year,
                EnrolledAt = enrollment.EnrolledAt,

            };
          
        }
        public async Task<EnrollmentReponseDto> CreateAsync(EnrollmentCreateDto dto)
        {
            // Validate  Student exists
            var student = await _context.Students.FindAsync(dto.StudentId);

            if(student == null)
            throw new NotFoundException($"Student {dto.StudentId} not found");

            // Validate Subject exists
            var subject = await _context.Subjects.FindAsync(dto.SubjectId);
            
            if (subject ==null)
            throw new NotFoundException($"Subject {dto.SubjectId} not found");

            var exists = await _context.Enrollments 
            .AnyAsync( e =>
              e.StudentId == dto.StudentId &&
              e.SubjectId == dto.SubjectId && 
              e.Semester == dto.Semester &&
              e.Year == dto.Year 
            );

            if (exists)
            throw new DuplicateException($"Student already enrolled in the subject");

            var enrollment = new Enrollment
            {
                StudentId = dto.StudentId,
                SubjectId = dto.SubjectId,
                Semester = dto.Semester,
                Year = dto.Year
            };

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();

            return new EnrollmentReponseDto
            {
                Id         = enrollment.Id,
                StudentId = enrollment.StudentId,
                StudentName = enrollment.Student.FullName,
                SubjectId = enrollment.SubjectId,
                SubjectName = enrollment.Subject.SubjectName,
                Semester = enrollment.Semester,
                Year = enrollment.Year,
                EnrolledAt = enrollment.EnrolledAt,
            };
        }

        public async Task DeleteAsync(int id )
        {
            var enrollment = await _context.Enrollments.FindAsync(id);

            if (enrollment == null) 
            throw new NotFoundException($"Enrollment {id} not found");

            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
        }
     }
}

