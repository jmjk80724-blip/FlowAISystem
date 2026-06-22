using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Feedback
{
    public class FeedbackResponseDto
    {
        public int Id {get; set;}
        public int PredictionId { get; set; }
        public bool IsCorrect { get; set; }
        public string Correction { get; set; } =string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}