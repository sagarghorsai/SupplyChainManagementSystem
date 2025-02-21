using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class UsersModel
    {
        public int UserId { get; set; }
        public string UserFirst { get; set; }
        public string UserLast { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string Role { get; set; } = "customer";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
