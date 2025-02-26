using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CapstoneProject.Components.Model
{
    public class RegisterUserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_id { get; set; }
        [Required]
        public string user_First { get; set; }
        [Required]
        public string user_Last { get; set; }
        [Required]
        public string user_Name { get; set; }
        [Required]
        public string user_Password { get; set; }
        public string Role { get; set; } = "customer";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
