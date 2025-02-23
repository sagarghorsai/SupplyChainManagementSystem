using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLibrary.Model
{
    public class UsersModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        public string UserFirst { get; set; }

        public string UserLast { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string Role { get; set; } = "customer";

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
