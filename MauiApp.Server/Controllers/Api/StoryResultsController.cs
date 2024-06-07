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
    public class StoryResultsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StoryResultsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/StoryResults
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StoryResult>>> GetStoryResults()
        {
          if (_context.StoryResults == null)
          {
              return NotFound();
          }
            return await _context.StoryResults.ToListAsync();
        }

        // GET: api/StoryResults/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StoryResult>> GetStoryResult(Guid id)
        {
          if (_context.StoryResults == null)
          {
              return NotFound();
          }
            var storyResult = await _context.StoryResults.FindAsync(id);

            if (storyResult == null)
            {
                return NotFound();
            }

            return storyResult;
        }

        // PUT: api/StoryResults/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStoryResult(Guid id, StoryResult storyResult)
        {
            if (id != storyResult.Id)
            {
                return BadRequest();
            }

            _context.Entry(storyResult).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoryResultExists(id))
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

        // POST: api/StoryResults
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StoryResult>> PostStoryResult(StoryResult storyResult)
        {
          if (_context.StoryResults == null)
          {
              return Problem("Entity set 'AppDbContext.StoryResults'  is null.");
          }
            _context.StoryResults.Add(storyResult);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStoryResult", new { id = storyResult.Id }, storyResult);
        }

        // DELETE: api/StoryResults/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStoryResult(Guid id)
        {
            if (_context.StoryResults == null)
            {
                return NotFound();
            }
            var storyResult = await _context.StoryResults.FindAsync(id);
            if (storyResult == null)
            {
                return NotFound();
            }

            _context.StoryResults.Remove(storyResult);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoryResultExists(Guid id)
        {
            return (_context.StoryResults?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
