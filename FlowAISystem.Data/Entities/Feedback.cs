using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowAISystem.Data.Entities
{
    [Table("Feedbacks")]
    public class Feedback
    {
            [Key]
        public int Id {get; set;} // Primary key

        [Required]
        [ForeignKey("Prediction")]
        public int PredictionId { get; set; } // Foreign key to Prediction

        [Required]
        public  bool IsCorrect { get; set; } // Indicates if the prediction was correct

        [StringLength(255)]
        public string Correction { get; set; } = string.Empty; // Optional correction or comment
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Timestamp
        // Navigation property
        public Prediction Prediction { get; set; }



    }
   
}