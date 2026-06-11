using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Score
{
    public class ScoreCreateDto
    {
        [Required(ErrorMessage = "EnrollmentId is required.")]
        public int EnrollmentId { get; set; }

        [Required(ErrorMessage = "ScoreType is required.")]
        [StringLength(20)]
        public string ScoreType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Value is required.")]
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
        public double Value { get; set; }
        
    }
}