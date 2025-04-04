using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.FoundItem
{
    public class CreateFoundItemDto
    {
        public string ItemName {get; set;} = string.Empty;
        public string Description {get; set;} = string.Empty;
        public DateOnly FoundDate {get; set;}
    }
}