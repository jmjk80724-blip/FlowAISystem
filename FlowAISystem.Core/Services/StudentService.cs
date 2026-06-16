
using FlowAISystem.Data.DTOs.Student;
using Microsoft.EntityFrameworkCore;
using FlowAISystem.Data;
using FlowAISystem.Data.Entities;
using FlowAISystem.Core.Services.Interface;
using FlowAISystem.Core.Exceptions;

namespace FlowAISystem.Core.Services
{
    public class StudentService : IStudentService // Anstration in oop
    {
        private readonly AppDbContext _context;  /// call AppDbContext

        public StudentService( AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<StudentResponseDto>> GetAllAsync() // Abstartion by Interface IStudent Task
        {
            return await _context.Students
                .Select(s => new StudentResponseDto
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Gender = s.Gender,
                    Email = s.Email,
                    PhoneNumber = s.PhoneNumber,
                    DateOfBirth = s.DateOfBirth,
                    CreateAt = s.CreatedAt
                })
                .ToListAsync();

        }
        public async Task<StudentResponseDto> GetByIdAsync( int id )
        {
            var student = await _context.Students.FindAsync(id);

            if (student = null)
            
                throw new NotFountException($"Student {id} not found");
                return new StudentResponseDto
                {
                    Id = student.Id,
                    FullName = student.FullName,
                    Gender = student.Gender,
                    Email = student.Email,
                    PhoneNumber = student.PhoneNumber,
                    DateOfBirth = student.DateOfBirth,
                    CreateAt = student.CreateAt
                };
            
        }
        public async Task<StudentResponseDto> CreateAsync (StudentCreateDto dto)
        {
            //Validation duplicate email 
            var exists = await _context.Students.AnyAsync( s =>s.Email == dto.Email);

            if (exists) 
            throw new DuplicateException("Email already exists");

            var student = new Student
            {
                FullName = dto.FullName,
                DateOfBirth = dto.DateOfBirth,
                Gender = dto.Gender,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };

            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            return new StudentResponseDto
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                CreateAt = student.CreatedAt
            };
        }
        public async Task UpdateAsync (int id , StudentUpdateDto dto)
        {
            var student = await _context.Students.FindAsync(id);

            if (student = null)
            throw new NotFountException($"Student {id} not found");

            student.FullName = dto.FullName ?? student.FullName;
            student.Gender = dto.Gender ?? student.Gender;
            student.PhoneNumber = dto.PhoneNumber ?? student.PhoneNumber;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if(student= null) 
            throw  new NotFountException($"Student {id} Not found");

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

    }
}