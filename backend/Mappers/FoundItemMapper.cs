using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.FoundItem;
using backend.Models;

namespace backend.Mappers
{
    public static class FoundItemMapper
    {
        public static FoundItemDto ToFoundItemDto(this FoundItem foundItemModel) 
        {
            return new FoundItemDto
            {
                FoundId = foundItemModel.FoundId,
                ItemName = foundItemModel.ItemName,
                Description = foundItemModel.Description,
                FoundDate = foundItemModel.FoundDate,
                Status = foundItemModel.Status
            };
        }
        public static FoundItem FoundItemFromCreateDto(this CreateFoundItemDto foundItemDto) 
        {
            return new FoundItem
            {
                ItemName = foundItemDto.ItemName,
                Description = foundItemDto.Description,
                FoundDate = foundItemDto.FoundDate,
                Status = "Unclaimed",
            };
        }
        
    }
}