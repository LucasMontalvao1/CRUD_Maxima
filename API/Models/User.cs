using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class User
    {
        [Key]
        public int id_usuario { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Email { get; set; }
    }
}
