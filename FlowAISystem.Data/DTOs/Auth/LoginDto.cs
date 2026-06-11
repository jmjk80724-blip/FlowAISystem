using System.ComponentModel.DataAnnotations;

namespace FlowAISystem.Data.DTOs.Auth {
    public class LoginDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }  = String.Empty;

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;
    }
}