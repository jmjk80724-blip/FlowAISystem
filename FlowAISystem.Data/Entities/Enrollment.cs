using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowAISystem.Data.Entities
{
  [Table("Enrollments")]
    public class Enrollment
    {
      [Key]
      public int Id { get; set;}

      [Required]
      [ForeignKey("Student")]
      public int StudentId { get; set; }

      [Required]
      [ForeignKey("Subject")]
      public int SubjectId { get; set; }

        [Required]
        [StringLength(20)]
      public string Semester { get; set; } = string.Empty;
      [Required]
      [Range(2000, 2100)]
      public int Year { get; set; }
      public DateTime EnrolledAt { get; set; } = DateTime.Now;

      // Navigation properties
        public Student Student { get; set; } 
        public Subject Subject { get; set; } 
        public ICollection<Score> Scores{ get; set; }
        public ICollection<Prediction> Predictions { get; set; }

    }
}