using System;

using FlowAISystem.Data.DTOs.Enrollment;
using FlowAISystem.Data.Entities;

namespace FlowAISystem.Core.Services.Interfaces
{
    public interface IEnrollmentService
    {
        Task <List<EnrollmentReponseDto>> GetAllAsync();
        Task <EnrollmentReponseDto> GetIdAsync(int id);
        Task <EnrollmentReponseDto> CreateAsync(EnrollmentCreateDto dto);
        Task DeleteAsync(int id);
    }
}