using FlowAISystem.Data.DTOs.Prediction;

namespace FlowAISystem.Core.Services.Interfaces
{
    public interface IPredictionService
    {
        Task<List<PredictionResponseDto>> GetAllAsync();
        Task<PredictionResponseDto> GetByEnrollmentIdAsync (int enrollmentid);
        Task<PredictionResponseDto> PredictAsync(PredictionRequestDto dto);
}
}