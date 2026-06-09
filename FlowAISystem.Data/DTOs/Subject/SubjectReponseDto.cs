using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Student
{
    public class SubjectReponseDto
    {
        public int Id {get; set;} 
        public string SubjectName {get; set;}
        public int Credits {get; set;}
        public string Description {get; set;}
    }
}