using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Student
{
    public class SubjectUpdateDto
    {
        [StringLength(100)]
        public string SubjectName {get;set;}
        [Range(1,6)]
        public int Credits {get;set;}

        [StringLength(255)]
        public string Description {get; set;}
    }
}