using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.LostItem
{
    public class UpdateLostItemStatusDto
    {
        public string Status { get; set; } = string.Empty; // "Lost" // "Found" // "Claimed"
    }
}