using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowAISystem.Data.Entities
{
    [Table("AIModels")]
    public class AIModel
    {
        [Key]
        public int Id { get; set;} // Primary key

        [Required]
        [StringLength(100)]
        public string ModelName { get; set;} = string.Empty; // Name of the AI

        [Required]
        [StringLength(100)]
        public string Version { get; set;} = string.Empty; // Version of the AI model

        [StringLength(255)]
        public string Description { get; set;} = string.Empty; // Description of the AI model

        [Range(0.0, 100.0)]
        public double Accuracy { get; set;} // Accuracy of the AI model
        
        public DateTime TrainedAt { get; set;} = DateTime.Now; // Timestamp of training
        // Navigation property
        public ICollection<TrainingData> TrainingDataList{ get; set; } // Collection of training data used for this model
        public ICollection<PredictionLog> PredictionLogs { get; set; } // Collection of prediction logs for this model
        public ICollection<Prediction> Predictions { get; set; } // Collection of predictions made by this model
    }
}