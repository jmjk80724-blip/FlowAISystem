using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowAISystem.Data.Entities
{
    [Table("PredictionLogs")]
    public class PredictionLog
    {
        [Key]
        public int Id {get; set;}
        [Required]
        [ForeignKey("AIModel")]
        public int AIModelId { get; set; }
        [Required]
        [ForeignKey("Prediction")]
        public int PredictionId { get; set; }

        [Required]
        public string Output { get; set;} = string.Empty;

        [Required]
        public string Input { get; set;} = string.Empty;
        
        [Range(0.0, 1.0)]
        public double Confidence { get; set;}
        public DateTime CreatedAt { get; set;} = DateTime.Now;

        public AIModel AIModel { get; set;}
        public Prediction Prediction { get; set;}


    }
}