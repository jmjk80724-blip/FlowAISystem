using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Student
{
    public class StudentUpdateDto
    {
        [StringLength(100)]
        public string FullName { get; set; }
        public string  Gender { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
    }
}