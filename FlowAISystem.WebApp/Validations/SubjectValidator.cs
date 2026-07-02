using FlowAISystem.Data.DTOs.Subject; // use the real namespace for your DTO

namespace FlowAISystem.WebApp.Validations
{
    public class SubjectValidator
    {
        public static string? Validate(SubjectCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.SubjectName))
            {
                return "Subject name is required.";
            }
            else if (dto.SubjectName.Length > 100)
            {
                return "Subject name cannot exceed 100 characters.";
            }
            if (dto.Credits < 1 || dto.Credits > 6)
            {
                return "Credits must be between 1 and 6.";
            }
           
           
        if (dto.Description != null && dto.Description.Length > 500)
            {
                return "Description cannot exceed 500 characters.";
            }
            return null;
        }
    }
}