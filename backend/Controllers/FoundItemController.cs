using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.FoundItem;
using backend.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("backend/founditems")]
    [ApiController]
    public class FoundItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public FoundItemController(ApplicationDbContext context)
        {
            _context = context;
        }
        //Everyone can see all found items
        [HttpGet, Authorize]
        public async Task<IActionResult> GetAll()
        {
            var founditems = await _context.FoundItems.ToListAsync();
            var foundItemDto = founditems.Select(s => s.ToFoundItemDto());

            return Ok(foundItemDto);
        }
        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var founditem = await _context.FoundItems.FindAsync(id);

            if (founditem == null) {
                return NotFound();
            }
            return Ok(founditem.ToFoundItemDto());
        }
        //Only admins can create found items
        [HttpPost, Authorize(Roles ="admin")]
        public async Task<IActionResult> Create([FromBody] CreateFoundItemDto foundItemDto)
        {
            var foundItemModel = foundItemDto.FoundItemFromCreateDto();
            //Add the found item to the database
            _context.FoundItems.Add(foundItemModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = foundItemModel.FoundId }, foundItemModel.ToFoundItemDto());
        }
        //Only admins can update found items
        [HttpPut, Authorize(Roles = "admin")]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CreateFoundItemDto foundItemDto)
        {
            var founditem = await _context.FoundItems.FindAsync(id);
            if (founditem == null) {
                return NotFound();
            }
            founditem.ItemName = foundItemDto.ItemName;
            founditem.Description = foundItemDto.Description;
            founditem.FoundDate = foundItemDto.FoundDate;

            await _context.SaveChangesAsync();
            return Ok(founditem.ToFoundItemDto());
        }
        //Only admins can delete found items
        [HttpDelete, Authorize(Roles = "admin")]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var founditem = await _context.FoundItems.FindAsync(id);
            if (founditem == null) {
                return NotFound();
            }
            _context.FoundItems.Remove(founditem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
    }
}