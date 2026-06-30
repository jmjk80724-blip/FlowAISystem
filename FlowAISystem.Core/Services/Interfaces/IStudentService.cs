using System;
using FlowAISystem.Data.DTOs.Student;
using FlowAISystem.Data.Entities;

namespace FlowAISystem.Core.Services.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentResponseDto>>  GetAllAsync(); 
        Task<StudentResponseDto> GetByIdAsync(int id);
        Task<StudentResponseDto> CreateAsync(StudentCreateDto dto);
        Task UpdateAsync(int id, StudentUpdateDto dto);
        Task DeleteAsync(int id);

    }
}

