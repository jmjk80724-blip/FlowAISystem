using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowAISystem.Data.Entities
{
    [Table("Subjects")]
    public class Subject
    {

        [Key]
        public int Id {get; set; }

        [Required]
        [StringLength(100)]
        public string SubjectName { get; set; } = string.Empty;
        [Required]
        [Range(1, 6)]
        public int Credits { get; set; }

        [StringLength(255)]
        public string Description { get; set; } = string.Empty;

        public ICollection<Enrollment> Enrollments { get; set; } 
    }
}