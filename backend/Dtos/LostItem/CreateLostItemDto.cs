using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.LostItem
{
    public class CreateLostItemDto
    {
        public string ItemName {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public DateTime LostDate {get; set;}
        public int UserId {get; set;}
    }
}