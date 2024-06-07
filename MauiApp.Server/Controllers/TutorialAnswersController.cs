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
    public class TutorialAnswersController : Controller
    {
        private readonly AppDbContext _context;

        public TutorialAnswersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TutorialAnswers
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.TutorialAnswers.Include(t => t.Question);
            return View(await appDbContext.ToListAsync());
        }

        // GET: TutorialAnswers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.TutorialAnswers == null)
            {
                return NotFound();
            }

            var tutorialAnswer = await _context.TutorialAnswers
                .Include(t => t.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tutorialAnswer == null)
            {
                return NotFound();
            }

            return View(tutorialAnswer);
        }

        // GET: TutorialAnswers/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Text");
            return View();
        }

        // POST: TutorialAnswers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IsCorrect,QuestionId,Text,Id")] TutorialAnswer tutorialAnswer)
        {
            if (ModelState.IsValid)
            {
                tutorialAnswer.Id = Guid.NewGuid();
                _context.Add(tutorialAnswer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Text", tutorialAnswer.QuestionId);
            return View(tutorialAnswer);
        }

        // GET: TutorialAnswers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.TutorialAnswers == null)
            {
                return NotFound();
            }

            var tutorialAnswer = await _context.TutorialAnswers.FindAsync(id);
            if (tutorialAnswer == null)
            {
                return NotFound();
            }
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Text", tutorialAnswer.QuestionId);
            return View(tutorialAnswer);
        }

        // POST: TutorialAnswers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IsCorrect,QuestionId,Text,Id")] TutorialAnswer tutorialAnswer)
        {
            if (id != tutorialAnswer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tutorialAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TutorialAnswerExists(tutorialAnswer.Id))
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
            ViewData["QuestionId"] = new SelectList(_context.Questions, "Id", "Text", tutorialAnswer.QuestionId);
            return View(tutorialAnswer);
        }

        // GET: TutorialAnswers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.TutorialAnswers == null)
            {
                return NotFound();
            }

            var tutorialAnswer = await _context.TutorialAnswers
                .Include(t => t.Question)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tutorialAnswer == null)
            {
                return NotFound();
            }

            return View(tutorialAnswer);
        }

        // POST: TutorialAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.TutorialAnswers == null)
            {
                return Problem("Entity set 'AppDbContext.TutorialAnswers'  is null.");
            }
            var tutorialAnswer = await _context.TutorialAnswers.FindAsync(id);
            if (tutorialAnswer != null)
            {
                _context.TutorialAnswers.Remove(tutorialAnswer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TutorialAnswerExists(Guid id)
        {
          return (_context.TutorialAnswers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
