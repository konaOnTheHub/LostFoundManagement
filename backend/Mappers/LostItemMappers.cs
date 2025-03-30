using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.LostItem;
using backend.Models;

namespace backend.Mappers
{
    public static class LostItemMappers
    {
        public static LostItemDto ToLostItemDto(this LostItem lostItemModel)
        {
            return new LostItemDto
            {
                LostId = lostItemModel.LostId,
                ItemName = lostItemModel.ItemName,
                Description = lostItemModel.Description,
                LostDate = lostItemModel.LostDate,
                Status = lostItemModel.Status,
                UserId = lostItemModel.UserId,
            };
        }
        public static LostItem LostItemFromCreateDto(this CreateLostItemDto lostItemDto, int loggedUserId)
        {
            return new LostItem
            {
                ItemName = lostItemDto.ItemName,
                Description = lostItemDto.Description,
                LostDate = DateTime.Now,
                Status = "Unclaimed",
                UserId = loggedUserId,
            };
        }
    }
}