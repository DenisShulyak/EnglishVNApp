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
    public class UserChaptersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserChaptersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserChapters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserChapter>>> GetUserChapters()
        {
          if (_context.UserChapters == null)
          {
              return NotFound();
          }
            return await _context.UserChapters.ToListAsync();
        }

        // GET: api/UserChapters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserChapter>> GetUserChapter(Guid id)
        {
          if (_context.UserChapters == null)
          {
              return NotFound();
          }
            var userChapter = await _context.UserChapters.FindAsync(id);

            if (userChapter == null)
            {
                return NotFound();
            }

            return userChapter;
        }

        // PUT: api/UserChapters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserChapter(Guid id, UserChapter userChapter)
        {
            if (id != userChapter.Id)
            {
                return BadRequest();
            }

            _context.Entry(userChapter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserChapterExists(id))
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

        // POST: api/UserChapters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserChapter>> PostUserChapter(UserChapter userChapter)
        {
          if (_context.UserChapters == null)
          {
              return Problem("Entity set 'AppDbContext.UserChapters'  is null.");
          }
            _context.UserChapters.Add(userChapter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserChapter", new { id = userChapter.Id }, userChapter);
        }

        // DELETE: api/UserChapters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserChapter(Guid id)
        {
            if (_context.UserChapters == null)
            {
                return NotFound();
            }
            var userChapter = await _context.UserChapters.FindAsync(id);
            if (userChapter == null)
            {
                return NotFound();
            }

            _context.UserChapters.Remove(userChapter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserChapterExists(Guid id)
        {
            return (_context.UserChapters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
