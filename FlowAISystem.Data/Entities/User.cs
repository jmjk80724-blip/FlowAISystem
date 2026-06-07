using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowAISystem.Data.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
       
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        [ForeignKey("Role")]
        public int RoleId { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Role Role { get; set; }
    }
}