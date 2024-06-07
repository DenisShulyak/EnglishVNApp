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
    public class ScenesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ScenesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Scenes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Scene>>> GetScenes()
        {
          if (_context.Scenes == null)
          {
              return NotFound();
          }
            return await _context.Scenes.ToListAsync();
        }

        // GET: api/Scenes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Scene>> GetScene(Guid id)
        {
          if (_context.Scenes == null)
          {
              return NotFound();
          }
            var scene = await _context.Scenes.FindAsync(id);

            if (scene == null)
            {
                return NotFound();
            }

            return scene;
        }

        // PUT: api/Scenes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutScene(Guid id, Scene scene)
        {
            if (id != scene.Id)
            {
                return BadRequest();
            }

            _context.Entry(scene).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SceneExists(id))
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

        // POST: api/Scenes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Scene>> PostScene(Scene scene)
        {
          if (_context.Scenes == null)
          {
              return Problem("Entity set 'AppDbContext.Scenes'  is null.");
          }
            _context.Scenes.Add(scene);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetScene", new { id = scene.Id }, scene);
        }

        // DELETE: api/Scenes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScene(Guid id)
        {
            if (_context.Scenes == null)
            {
                return NotFound();
            }
            var scene = await _context.Scenes.FindAsync(id);
            if (scene == null)
            {
                return NotFound();
            }

            _context.Scenes.Remove(scene);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SceneExists(Guid id)
        {
            return (_context.Scenes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
