using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Dtos.FoundItem
{
    public class FoundItemDto
    {
        public int FoundId {get; set;}
        public string ItemName {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public DateOnly FoundDate {get; set;} 
        public string Status {get; set;} = "Unclaimed"; //claimed
    }
}