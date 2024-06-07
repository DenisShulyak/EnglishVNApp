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
    public class SceneTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SceneTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SceneTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SceneType>>> GetSceneTypes()
        {
          if (_context.SceneTypes == null)
          {
              return NotFound();
          }
            return await _context.SceneTypes.ToListAsync();
        }

        // GET: api/SceneTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SceneType>> GetSceneType(Guid id)
        {
          if (_context.SceneTypes == null)
          {
              return NotFound();
          }
            var sceneType = await _context.SceneTypes.FindAsync(id);

            if (sceneType == null)
            {
                return NotFound();
            }

            return sceneType;
        }

        // PUT: api/SceneTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSceneType(Guid id, SceneType sceneType)
        {
            if (id != sceneType.Id)
            {
                return BadRequest();
            }

            _context.Entry(sceneType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SceneTypeExists(id))
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

        // POST: api/SceneTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SceneType>> PostSceneType(SceneType sceneType)
        {
          if (_context.SceneTypes == null)
          {
              return Problem("Entity set 'AppDbContext.SceneTypes'  is null.");
          }
            _context.SceneTypes.Add(sceneType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSceneType", new { id = sceneType.Id }, sceneType);
        }

        // DELETE: api/SceneTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSceneType(Guid id)
        {
            if (_context.SceneTypes == null)
            {
                return NotFound();
            }
            var sceneType = await _context.SceneTypes.FindAsync(id);
            if (sceneType == null)
            {
                return NotFound();
            }

            _context.SceneTypes.Remove(sceneType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SceneTypeExists(Guid id)
        {
            return (_context.SceneTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
