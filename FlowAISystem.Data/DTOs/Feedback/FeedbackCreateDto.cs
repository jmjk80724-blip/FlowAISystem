using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Feedback
{
    public class FeedbackCreateDto
    {
        
        [Required(ErrorMessage = "PredicionId is required.")]
        public int PredictionId { get; set; }

        [Required(ErrorMessage ="IsCorrect is required.")]
        public bool IsCorrect {get;set;}
 
        [StringLength(500)]
        public string Correction {get;set;} = string.Empty;
    }
}