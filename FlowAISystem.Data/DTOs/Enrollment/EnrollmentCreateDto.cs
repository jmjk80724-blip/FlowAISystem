using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Enrollment
{
    public class EnrollmentCreateDto
    {
        [Required(ErrorMessage = "StudentId is required.")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "SubjectId is required.")]
        public int SubjectId { get; set; }

        [Required(ErrorMessage = "Semester is required.")]
        [StringLength(20, ErrorMessage = "Semester cannot be longer than 20 characters.")]
        public string Semester { get; set; } = string.Empty;


        [Required(ErrorMessage = "Year is required.")]
        [Range(2000, 2100, ErrorMessage = "Invalid year.")]
        public int Year { get; set; }
    }
}