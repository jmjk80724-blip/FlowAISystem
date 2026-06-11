using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Prediction
{
    public class PredictionResponseDto
    {
        public int Id { get; set;}
        public int EnrollmentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public double AverageScore { get; set; }
        public string Result { get; set; } = string.Empty;
        public string Recommendation { get; set; } = string.Empty;
        public string AIModelName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
    
}