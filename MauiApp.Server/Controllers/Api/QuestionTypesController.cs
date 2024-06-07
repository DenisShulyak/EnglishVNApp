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
    public class QuestionTypesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuestionTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/QuestionTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionType>>> GetQuestionTypes()
        {
          if (_context.QuestionTypes == null)
          {
              return NotFound();
          }
            return await _context.QuestionTypes.ToListAsync();
        }

        // GET: api/QuestionTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionType>> GetQuestionType(Guid id)
        {
          if (_context.QuestionTypes == null)
          {
              return NotFound();
          }
            var questionType = await _context.QuestionTypes.FindAsync(id);

            if (questionType == null)
            {
                return NotFound();
            }

            return questionType;
        }

        // PUT: api/QuestionTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionType(Guid id, QuestionType questionType)
        {
            if (id != questionType.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionTypeExists(id))
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

        // POST: api/QuestionTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<QuestionType>> PostQuestionType(QuestionType questionType)
        {
          if (_context.QuestionTypes == null)
          {
              return Problem("Entity set 'AppDbContext.QuestionTypes'  is null.");
          }
            _context.QuestionTypes.Add(questionType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionType", new { id = questionType.Id }, questionType);
        }

        // DELETE: api/QuestionTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionType(Guid id)
        {
            if (_context.QuestionTypes == null)
            {
                return NotFound();
            }
            var questionType = await _context.QuestionTypes.FindAsync(id);
            if (questionType == null)
            {
                return NotFound();
            }

            _context.QuestionTypes.Remove(questionType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionTypeExists(Guid id)
        {
            return (_context.QuestionTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
