using FlowAISystem.Data.DTOs.Score;

namespace FlowAISystem.Core.Services.Interfaces
{
    public interface IScoreService
    {
        Task <List<ScoreResponseDto>> GetAllAsync();      
        Task <ScoreResponseDto> GetEnrollmentIdAsync(int enrollmentId );
        Task <ScoreResponseDto> CreateAysync(ScoreCreateDto dto);
        Task UpdateAsync(int id, ScoreUpdateDto dto);
        Task DeleteAsynce(int id);
    }
}