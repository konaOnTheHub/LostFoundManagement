using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username {get; set;} = string.Empty;
        public string PasswordHash {get; set;} = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Role { get; set; } = "user"; // 'admin', 'user'
        public ICollection<Claim> Claims {get;} = new List<Claim>();
        public ICollection<LostItem> LostItems {get;} = new List<LostItem>();
    }
}