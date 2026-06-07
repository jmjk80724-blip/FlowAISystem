using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowAISystem.Data.Entities
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        public int Id {get; set; }
        [Required]
        [StringLength(50)]
        public string RoleName { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; }
    }
}