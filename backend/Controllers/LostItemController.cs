using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.LostItem;
using backend.Mappers;
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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lostitems = await _context.LostItems.ToListAsync();
            var lostItemDto = lostitems.Select(s => s.ToLostItemDto());

            return Ok(lostitems);
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
            //Extract the NameIdentifier claim from the JWT token which is the PK of the user in the database
            var loggedUserString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //Convert to INT as claims are stored in string
            var loggedUserId = int.Parse(loggedUserString!);
            //Pass in the logged user ID to the mapper method to create the lost item model
            var lostItemModel = lostItemDto.LostItemFromCreateDto(loggedUserId);
            await _context.LostItems.AddAsync(lostItemModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new {id = lostItemModel.LostId}, lostItemModel.ToLostItemDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateLostItemDto updateDto)
        {
            var lostModel = await _context.LostItems.FirstOrDefaultAsync(x => x.LostId == id);
            if (lostModel == null) {
                return NotFound();
            }
            lostModel.ItemName = updateDto.ItemName;
            lostModel.Description = updateDto.Description;
            lostModel.LostDate = updateDto.LostDate;

            await _context.SaveChangesAsync();

            return Ok(lostModel.ToLostItemDto());
        }
    }
}