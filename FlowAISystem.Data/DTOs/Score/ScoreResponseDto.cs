using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Score
{
    public class ScoreResponseDto
    {
        public int Id { get; set;}
        public int EnrollmentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
        public string ScoreType { get; set; } = string.Empty;
        public double Value { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}