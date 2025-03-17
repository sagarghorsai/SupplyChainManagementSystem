using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.Model
{
    public class UsersModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_id { get; set; }

        public string user_Name { get; set; }

        public string user_Last { get; set; }

        public string user_First { get; set; }
        public string Email { get; set; } 

        public string user_Password { get; set; }

        public string Role { get; set; } = "customer";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
