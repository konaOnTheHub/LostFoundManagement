using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.LostItem;
using backend.Mappers;
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

        [HttpPost]
        public IActionResult Create([FromBody] CreateLostItemDto lostItemDto)
        {
            var lostItemModel = lostItemDto.LostItemFromCreateDto();
            _context.LostItems.Add(lostItemModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id = lostItemModel.LostId}, lostItemModel.ToLostItemDto());
        }
    }
}