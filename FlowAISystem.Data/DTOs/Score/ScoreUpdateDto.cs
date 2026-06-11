using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Score
{
    public class ScoreUpdateDto
    {

        
        [Range(0, 100, ErrorMessage = "Value must be between 0 and 100.")]
        public double Value { get; set; }
    }
}