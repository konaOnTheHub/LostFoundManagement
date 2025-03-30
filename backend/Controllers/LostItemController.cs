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
        public IActionResult GetAll()
        {
            var lostitems = _context.LostItems.ToList().Select(s => s.ToLostItemDto());

            return Ok(lostitems);
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var lostitem = _context.LostItems.Find(id);

            if (lostitem == null) {
                return NotFound();
            }
            return Ok(lostitem.ToLostItemDto());
        }
        [HttpPost, Authorize]
        public IActionResult Create([FromBody] CreateLostItemDto lostItemDto)
        {
            //Extract the NameIdentifier claim from the JWT token which is the PK of the user in the database
            var loggedUserString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //Convert to INT as claims are stored in string
            var loggedUserId = int.Parse(loggedUserString!);
            //Pass in the logged user ID to the mapper method to create the lost item model
            var lostItemModel = lostItemDto.LostItemFromCreateDto(loggedUserId);
            _context.LostItems.Add(lostItemModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id = lostItemModel.LostId}, lostItemModel.ToLostItemDto());
        }
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateLostItemDto updateDto)
        {
            var lostModel = _context.LostItems.FirstOrDefault(x => x.LostId == id);
            if (lostModel == null) {
                return NotFound();
            }
            lostModel.ItemName = updateDto.ItemName;
            lostModel.Description = updateDto.Description;
            lostModel.LostDate = updateDto.LostDate;

            _context.SaveChanges();

            return Ok(lostModel.ToLostItemDto());
        }
    }
}