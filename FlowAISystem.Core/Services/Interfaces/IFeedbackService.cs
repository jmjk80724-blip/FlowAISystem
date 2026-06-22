using FlowAISystem.Data.DTOs.Feedback;


namespace FlowAISystem.Core.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<List<FeedbackResponseDto>> GetAllAsync();
        Task<FeedbackResponseDto> CreateAsync(FeedbackCreateDto dto);
    }
}