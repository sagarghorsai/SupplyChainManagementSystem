using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace CapstoneProject.Components.Model
{
    public class DisplayUserModel
    {
        [Required(AllowEmptyStrings =false, ErrorMessage ="Please provide User Name")]
        public string? UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Password")]
        public string? Password { get; set; }
    }
}
