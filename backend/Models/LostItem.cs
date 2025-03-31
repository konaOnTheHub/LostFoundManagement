using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class LostItem
    {
        [Key]
        public int LostId {get; set;}
        public string ItemName {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public DateOnly LostDate {get; set;}
        public string Status {get; set;} = "Unclaimed"; //Found //Claimed //Unclaimed
        public int UserId {get; set;} //Foreign key to users table
        public User User {get; set;} = null!; //Navigation property
    }
}