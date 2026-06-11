using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Enrollment
{
    public class EnrollmentReponseDto
    {
        public int Id {get; set;}

        public int StudentId {get; set; }

        public string StudentName { get; set; } = string.Empty;

        public int SubjectId { get; set; }

        public string SubjectName { get; set; } = string.Empty;

        public string Semester { get; set; } = string.Empty;

        public int Year { get; set; }

        public DateTime EnrollmentAt { get; set; }
    }
}