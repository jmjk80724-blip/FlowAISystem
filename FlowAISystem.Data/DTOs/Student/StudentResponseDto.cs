using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Student
{
    public class StudentResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreateAt {get; set;}
}
}