using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowAISystem.Data.Entities
{
    [Table("Students")]
    public class Student
    {
        [Key] // Primary key
        public int Id { get; set; }

        [Required] // Full name is required
        [StringLength(100)] // Maximum length of 100 characters
        public string? FullName { get; set; } 

        [Required] // Date of birth is required
        public DateTime DateOfBirth { get; set; }

        [Required] // Gender is required
        [StringLength(10)] // Maximum length of 10 characters 
        public string? Gender { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string? Email { get; set; }

        [Phone] // Validates that the input is a phone number
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [Required] // Creation date is required
        public DateTime CreatedAt { get; set; } = DateTime.Now; // Automatically set the creation date to the current date and time
        public ICollection<Enrollment> Enrollments { get; set; }

    }
}