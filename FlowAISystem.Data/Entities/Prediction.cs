using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowAISystem.Data.Entities
{
    [Table("Predictions")]
    public class Prediction
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Enrollment")]
        public int EnrollmentId { get; set; }
        [Required]
        [ForeignKey("AIModel")]
        public int AIModelId { get; set; }
        [Required]
        [Range(0, 100)]
        public double AverageScore { get; set; }
        [Required]
        [StringLength(10)]
        public string Result { get; set; } = string.Empty;
        [StringLength(255)]
        public string Recommendation { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        // Navigation property
        public Enrollment? Enrollment { get; set; }
        public AIModel? AIModel { get; set; }
        public Feedback? Feedback { get; set; }
        public ICollection<PredictionLog>? PredictionLogs { get; set; }
    }
}