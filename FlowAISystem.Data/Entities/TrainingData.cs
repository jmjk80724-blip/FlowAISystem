using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowAISystem.Data.Entities
{
    [Table("TrainingData")]
    public class TrainingData
    {
        [Key]
        public int Id {get; set;}
        [Required]
        [ForeignKey("AIModel")]
        public int AIModelId { get; set; }
        [Required]
        [Range(0, 100)]
       public double AverageScore { get; set;} 
       [Required]
       [StringLength(10)]
       public string Label { get; set;} = string.Empty;

         [StringLength(50)]
       public string Semester { get; set;} = string.Empty;
       public DateTime CreatedAt { get; set;} = DateTime.Now;
       public AIModel AIModel { get; set;}
    }
}