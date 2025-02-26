using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Components.Model
{
    public class LoginUserModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide User Name")]
        public string? UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Password")]
        public string? Password { get; set; }

    }
}
