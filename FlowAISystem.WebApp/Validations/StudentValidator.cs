

using FlowAISystem.Data.DTOs.Student;

namespace FlowAISystem.WebApp.Validations
{
    public class StudentValidator
    { 
        public static string?  Validate(StudentCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.FullName))
            {
                return "Full name is required.";
            }
             if (string.IsNullOrWhiteSpace(dto.Email) || !dto.Email.Contains("@"))
            {
                return "A valid email address is required.";
            }
            else if (dto.Email.Length > 100)
            {
                return "Email address cannot exceed 100 characters.";
            }
           
            if (dto.DateOfBirth == DateTime.MinValue)
            {
                return "Date of birth is required.";
            }
            if (dto.DateOfBirth > DateTime.Now)
            {
                return "Date of birth cannot be in the future.";
            }

            if (string.IsNullOrWhiteSpace(dto.Gender) || (dto.Gender != "Male" && dto.Gender != "Female"))
            {
                return "Gender must be either 'Male' or 'Female'.";
            }

            if (string.IsNullOrWhiteSpace(dto.PhoneNumber))
            {
                return "Phone number is required.";
            }
            else if (dto.PhoneNumber.Length <9 || dto.PhoneNumber.Length > 15)
            {
                return "Phone number must be between 9 and 15 characters.";
            }
            else if (!System.Text.RegularExpressions.Regex.IsMatch(dto.PhoneNumber, @"^\+?\d+$"))
            {
                return "Phone number can only contain digits and an optional leading '+'.";
            }
            
            return null; // No validation errors
            
        }
    }
}