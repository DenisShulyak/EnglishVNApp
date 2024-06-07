using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MauiApp.Data;
using MauiApp.Data.Models;

namespace MauiApp.Server.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ImageTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ImageTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImageType>>> GetImageTypes()
        {
          if (_context.ImageTypes == null)
          {
              return NotFound();
          }
            return await _context.ImageTypes.ToListAsync();
        }

        // GET: api/ImageTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImageType>> GetImageType(Guid id)
        {
          if (_context.ImageTypes == null)
          {
              return NotFound();
          }
            var imageType = await _context.ImageTypes.FindAsync(id);

            if (imageType == null)
            {
                return NotFound();
            }

            return imageType;
        }

        // PUT: api/ImageTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImageType(Guid id, ImageType imageType)
        {
            if (id != imageType.Id)
            {
                return BadRequest();
            }

            _context.Entry(imageType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ImageTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImageType>> PostImageType(ImageType imageType)
        {
          if (_context.ImageTypes == null)
          {
              return Problem("Entity set 'AppDbContext.ImageTypes'  is null.");
          }
            _context.ImageTypes.Add(imageType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImageType", new { id = imageType.Id }, imageType);
        }

        // DELETE: api/ImageTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImageType(Guid id)
        {
            if (_context.ImageTypes == null)
            {
                return NotFound();
            }
            var imageType = await _context.ImageTypes.FindAsync(id);
            if (imageType == null)
            {
                return NotFound();
            }

            _context.ImageTypes.Remove(imageType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImageTypeExists(Guid id)
        {
            return (_context.ImageTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
