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
    public class TutorialAnswersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TutorialAnswersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TutorialAnswers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TutorialAnswer>>> GetTutorialAnswers()
        {
          if (_context.TutorialAnswers == null)
          {
              return NotFound();
          }
            return await _context.TutorialAnswers.ToListAsync();
        }

        // GET: api/TutorialAnswers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TutorialAnswer>> GetTutorialAnswer(Guid id)
        {
          if (_context.TutorialAnswers == null)
          {
              return NotFound();
          }
            var tutorialAnswer = await _context.TutorialAnswers.FindAsync(id);

            if (tutorialAnswer == null)
            {
                return NotFound();
            }

            return tutorialAnswer;
        }

        // PUT: api/TutorialAnswers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTutorialAnswer(Guid id, TutorialAnswer tutorialAnswer)
        {
            if (id != tutorialAnswer.Id)
            {
                return BadRequest();
            }

            _context.Entry(tutorialAnswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TutorialAnswerExists(id))
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

        // POST: api/TutorialAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TutorialAnswer>> PostTutorialAnswer(TutorialAnswer tutorialAnswer)
        {
          if (_context.TutorialAnswers == null)
          {
              return Problem("Entity set 'AppDbContext.TutorialAnswers'  is null.");
          }
            _context.TutorialAnswers.Add(tutorialAnswer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTutorialAnswer", new { id = tutorialAnswer.Id }, tutorialAnswer);
        }

        // DELETE: api/TutorialAnswers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTutorialAnswer(Guid id)
        {
            if (_context.TutorialAnswers == null)
            {
                return NotFound();
            }
            var tutorialAnswer = await _context.TutorialAnswers.FindAsync(id);
            if (tutorialAnswer == null)
            {
                return NotFound();
            }

            _context.TutorialAnswers.Remove(tutorialAnswer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TutorialAnswerExists(Guid id)
        {
            return (_context.TutorialAnswers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
