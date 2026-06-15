using FlowAISystem.Data.DTOs.Prediction;

namespace FlowAISystem.Core.Services.Interfaces
{
    public interface IPredictionService
    {
        Task<List<PredictionRequestDto>> GetAllAsync();
        Task<PredictionRequestDto> GetIdAsync (int id);
        Task<PredictionRequestDto> PredictAsync(PredictionRequestDto dto);
}
}