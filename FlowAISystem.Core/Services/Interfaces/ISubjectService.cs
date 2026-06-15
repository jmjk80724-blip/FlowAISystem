using System;
using FlowAISystem.Data.DTOs.Student;
using FlowAISystem.Data.DTOs.Subject;


namespace FlowAISystem.Core.Services.Interfaces
{
    public interface ISubjectService
    {
        Task<List<SubjectResponseDto>> GetAllAsync()  ;
        Task<SubjectResponseDto> GetByIdAsync( int id);   
        Task<SubjectResponseDto> CreateAsync( SubjectCreateDto dto);
        Task UpdateAsync(int id, SubjectUpdateDto dto);
        Task DeleteAsync(int id);
    }
}