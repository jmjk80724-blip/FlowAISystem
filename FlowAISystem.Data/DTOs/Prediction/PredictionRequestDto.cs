using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Prediction
{
    public class PredictionRequestDto
    {
        [Required(ErrorMessage = "EnrollmentId is required.")]
        public int EnrollmentId { get; set; }
    }
    
}