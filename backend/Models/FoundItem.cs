using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class FoundItem
    {
        [Key]
        public int FoundId {get; set;}
        public string ItemName {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public DateOnly FoundDate {get; set;} 
        public string Status {get; set;} = "Unclaimed"; //claimed
        public ICollection<Claim> Claims { get; } = new List<Claim>();

    }
}