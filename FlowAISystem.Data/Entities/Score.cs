using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowAISystem.Data.Entities
{
    [Table("Scores")]
    public class Score
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Enrollment")]
        public int EnrollmentId { get; set; }
        [Required]
        [StringLength(20)]
        public string ScoreType { get; set; } = string.Empty;
        [Required]
        [Range(0, 100)]
        public double Value { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property
        public Enrollment Enrollment { get; set; }

    }
}

