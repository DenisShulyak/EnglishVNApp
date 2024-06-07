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
    public class ImpactAnswersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ImpactAnswersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/ImpactAnswers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImpactAnswer>>> GetImpactAnswers()
        {
          if (_context.ImpactAnswers == null)
          {
              return NotFound();
          }
            return await _context.ImpactAnswers.ToListAsync();
        }

        // GET: api/ImpactAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImpactAnswer>> GetImpactAnswer(Guid id)
        {
          if (_context.ImpactAnswers == null)
          {
              return NotFound();
          }
            var impactAnswer = await _context.ImpactAnswers.FindAsync(id);

            if (impactAnswer == null)
            {
                return NotFound();
            }

            return impactAnswer;
        }

        // PUT: api/ImpactAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImpactAnswer(Guid id, ImpactAnswer impactAnswer)
        {
            if (id != impactAnswer.Id)
            {
                return BadRequest();
            }

            _context.Entry(impactAnswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImpactAnswerExists(id))
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

        // POST: api/ImpactAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImpactAnswer>> PostImpactAnswer(ImpactAnswer impactAnswer)
        {
          if (_context.ImpactAnswers == null)
          {
              return Problem("Entity set 'AppDbContext.ImpactAnswers'  is null.");
          }
            _context.ImpactAnswers.Add(impactAnswer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImpactAnswer", new { id = impactAnswer.Id }, impactAnswer);
        }

        // DELETE: api/ImpactAnswers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImpactAnswer(Guid id)
        {
            if (_context.ImpactAnswers == null)
            {
                return NotFound();
            }
            var impactAnswer = await _context.ImpactAnswers.FindAsync(id);
            if (impactAnswer == null)
            {
                return NotFound();
            }

            _context.ImpactAnswers.Remove(impactAnswer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImpactAnswerExists(Guid id)
        {
            return (_context.ImpactAnswers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
