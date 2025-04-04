using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.LostItem;
using backend.Mappers;
using backend.Services.JWTClaim;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("backend/lostitems")]
    [ApiController]
    public class LostItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LostItemController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Only admins can see all lost items
        [HttpGet, Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAll()
        {
            var lostitems = await _context.LostItems.ToListAsync();
            var lostItemDto = lostitems.Select(s => s.ToLostItemDto());

            return Ok(lostitems);
        }
        //Anyone can see their own lost items
        [HttpGet("myLostItems"), Authorize]
        public async Task<IActionResult> GetAllUserLostItems()
        {
            //Extract UserId from the JWT token using service
            var loggedUserId = ExtractClaimService.ExtractNameIdentifier(User);
            var lostitems = await _context.LostItems.Where(x => x.UserId == loggedUserId).ToListAsync();
            var lostItemDto = lostitems.Select(s => s.ToLostItemDto());

            return Ok(lostItemDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var lostitem = await _context.LostItems.FindAsync(id);

            if (lostitem == null) {
                return NotFound();
            }
            return Ok(lostitem.ToLostItemDto());
        }
        [HttpPost, Authorize]
        public async Task<IActionResult> Create([FromBody] CreateLostItemDto lostItemDto)
        {
            //Extract UserId from the JWT token using service
            var loggedUserId = ExtractClaimService.ExtractNameIdentifier(User);
            //Pass in the logged user ID to the mapper method to create the lost item model
            var lostItemModel = lostItemDto.LostItemFromCreateDto(loggedUserId);
            await _context.LostItems.AddAsync(lostItemModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id = lostItemModel.LostId}, lostItemModel.ToLostItemDto());
        }
        [HttpPut, Authorize]
        [Route("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateLostItemDto updateDto)
        {
            //Extract UserId from the JWT token using service
            var loggedUserId = ExtractClaimService.ExtractNameIdentifier(User);
            var lostModel = await _context.LostItems.FirstOrDefaultAsync(x => x.LostId == id);
            if (lostModel == null) {
                return NotFound();
            }
            if (lostModel.UserId != loggedUserId) {
                return Unauthorized("You are not authorized to update this lost item.");
            }
            lostModel.ItemName = updateDto.ItemName;
            lostModel.Description = updateDto.Description;
            lostModel.LostDate = updateDto.LostDate;

            await _context.SaveChangesAsync();

            return Ok(lostModel.ToLostItemDto());
        }
        [HttpPut, Authorize(Roles = "admin")]
        [Route("updateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus([FromRoute] int id, [FromBody] UpdateLostItemStatusDto updateDto)
        {
            var lostModel = await _context.LostItems.FirstOrDefaultAsync(x => x.LostId == id);
            if (lostModel == null) {
                return NotFound();
            }
            lostModel.Status = updateDto.Status;
            await _context.SaveChangesAsync();

            return Ok(lostModel.ToLostItemDto());
        }
        //todo - add authorization to delete lost item
    }
}