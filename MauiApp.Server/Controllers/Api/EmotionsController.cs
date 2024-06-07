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
    public class EmotionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EmotionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Emotions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Emotion>>> GetEmotions()
        {
          if (_context.Emotions == null)
          {
              return NotFound();
          }
            return await _context.Emotions.ToListAsync();
        }

        // GET: api/Emotions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Emotion>> GetEmotion(Guid id)
        {
          if (_context.Emotions == null)
          {
              return NotFound();
          }
            var emotion = await _context.Emotions.FindAsync(id);

            if (emotion == null)
            {
                return NotFound();
            }

            return emotion;
        }

        // PUT: api/Emotions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmotion(Guid id, Emotion emotion)
        {
            if (id != emotion.Id)
            {
                return BadRequest();
            }

            _context.Entry(emotion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmotionExists(id))
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

        // POST: api/Emotions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Emotion>> PostEmotion(Emotion emotion)
        {
          if (_context.Emotions == null)
          {
              return Problem("Entity set 'AppDbContext.Emotions'  is null.");
          }
            _context.Emotions.Add(emotion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmotion", new { id = emotion.Id }, emotion);
        }

        // DELETE: api/Emotions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmotion(Guid id)
        {
            if (_context.Emotions == null)
            {
                return NotFound();
            }
            var emotion = await _context.Emotions.FindAsync(id);
            if (emotion == null)
            {
                return NotFound();
            }

            _context.Emotions.Remove(emotion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmotionExists(Guid id)
        {
            return (_context.Emotions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
