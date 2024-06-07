using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MauiApp.Data;
using MauiApp.Data.Models;

namespace MauiApp.Server.Controllers
{
    public class ImpactAnswersController : Controller
    {
        private readonly AppDbContext _context;

        public ImpactAnswersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ImpactAnswers
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.ImpactAnswers.Include(i => i.Emotion).Include(i => i.Question);
            return View(await appDbContext.ToListAsync());
        }

        // GET: ImpactAnswers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ImpactAnswers == null)
            {
                return NotFound();
            }

            var impactAnswer = await _context.ImpactAnswers
                .Include(i => i.Emotion)
                .Include(i => i.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (impactAnswer == null)
            {
                return NotFound();
            }

            return View(impactAnswer);
        }

        // GET: ImpactAnswers/Create
        public IActionResult Create()
        {
            ViewData["EmotionId"] = new SelectList(_context.Emotions, "Id", "Name");
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Text");
            return View();
        }

        // POST: ImpactAnswers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmotionId,QuestionId,Text,Id")] ImpactAnswer impactAnswer)
        {
            if (ModelState.IsValid)
            {
                impactAnswer.Id = Guid.NewGuid();
                _context.Add(impactAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmotionId"] = new SelectList(_context.Emotions, "Id", "Name", impactAnswer.EmotionId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Text", impactAnswer.QuestionId);
            return View(impactAnswer);
        }

        // GET: ImpactAnswers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ImpactAnswers == null)
            {
                return NotFound();
            }

            var impactAnswer = await _context.ImpactAnswers.FindAsync(id);
            if (impactAnswer == null)
            {
                return NotFound();
            }
            ViewData["EmotionId"] = new SelectList(_context.Emotions, "Id", "Name", impactAnswer.EmotionId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Text", impactAnswer.QuestionId);
            return View(impactAnswer);
        }

        // POST: ImpactAnswers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EmotionId,QuestionId,Text,Id")] ImpactAnswer impactAnswer)
        {
            if (id != impactAnswer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(impactAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImpactAnswerExists(impactAnswer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmotionId"] = new SelectList(_context.Emotions, "Id", "Name", impactAnswer.EmotionId);
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Text", impactAnswer.QuestionId);
            return View(impactAnswer);
        }

        // GET: ImpactAnswers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ImpactAnswers == null)
            {
                return NotFound();
            }

            var impactAnswer = await _context.ImpactAnswers
                .Include(i => i.Emotion)
                .Include(i => i.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (impactAnswer == null)
            {
                return NotFound();
            }

            return View(impactAnswer);
        }

        // POST: ImpactAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ImpactAnswers == null)
            {
                return Problem("Entity set 'AppDbContext.ImpactAnswers'  is null.");
            }
            var impactAnswer = await _context.ImpactAnswers.FindAsync(id);
            if (impactAnswer != null)
            {
                _context.ImpactAnswers.Remove(impactAnswer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImpactAnswerExists(Guid id)
        {
          return (_context.ImpactAnswers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
