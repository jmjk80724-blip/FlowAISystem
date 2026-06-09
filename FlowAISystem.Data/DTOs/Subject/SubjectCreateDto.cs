using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Student
{
    public class SubjectCreateDto
    {
        [Required(ErrorMessage ="SubjectName is required")]
        [StringLength(100)]
        public string SubjectName {get; set;}
        
        [Required(ErrorMessage = "Credits is requied")]
        [Range(1, 6, ErrorMessage = "Credits must be 1-6")]
        public int Credits {get; set;}

        [StringLength(255)]
        public string Description {get; set;}

    }
}