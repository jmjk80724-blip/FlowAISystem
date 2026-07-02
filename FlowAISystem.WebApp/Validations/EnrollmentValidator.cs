
namespace FlowAISystem.Data.DTOs.Enrollment
{
    public class EnrollmentValidator
    {
        public static string? Validate(EnrollmentCreateDto dto)
        {
            if (dto.StudentId <= 0)
            {
                return "Student ID must be a positive integer.";
            }
            if (dto.Year < 2000 || dto.Year > DateTime.Now.Year)
            {
                return "Year must be between 2000 and the current year.";
            }

            return null; // No validation errors
        }
    }
}