using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.LostItem
{
    public class LostItemDto
    {
        public int LostId {get; set;}
        public string ItemName {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public DateOnly LostDate {get; set;}
        public string Status {get; set;} = "Lost"; //Found //Claimed 
        public int UserId {get; set;} //Foreign key to users table
    }
}