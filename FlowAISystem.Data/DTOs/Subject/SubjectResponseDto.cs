using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Subject
{
    public class SubjectResponseDto
    {
        public int Id { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public int Credits { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}