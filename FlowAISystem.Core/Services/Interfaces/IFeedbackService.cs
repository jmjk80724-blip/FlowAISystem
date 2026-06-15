using FlowAISystem.Data.DTOs.Feedback;

namespace FlowAISystem.Core.Services.Interfaces
{
    public interface  IFeedbackService
    {
        Task<List<FeedbackResponsDto>> GetAllAsycn();
        Task<FeedbackResponsDto> CreateAsycn(FeedbackCreateDto dto);
    }
}