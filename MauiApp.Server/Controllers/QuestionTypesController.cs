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
    public class QuestionTypesController : Controller
    {
        private readonly AppDbContext _context;

        public QuestionTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: QuestionTypes
        public async Task<IActionResult> Index()
        {
              return _context.QuestionTypes != null ? 
                          View(await _context.QuestionTypes.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.QuestionTypes'  is null.");
        }

        // GET: QuestionTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.QuestionTypes == null)
            {
                return NotFound();
            }

            var questionType = await _context.QuestionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionType == null)
            {
                return NotFound();
            }

            return View(questionType);
        }

        // GET: QuestionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: QuestionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] QuestionType questionType)
        {
            if (ModelState.IsValid)
            {
                questionType.Id = Guid.NewGuid();
                _context.Add(questionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(questionType);
        }

        // GET: QuestionTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.QuestionTypes == null)
            {
                return NotFound();
            }

            var questionType = await _context.QuestionTypes.FindAsync(id);
            if (questionType == null)
            {
                return NotFound();
            }
            return View(questionType);
        }

        // POST: QuestionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] QuestionType questionType)
        {
            if (id != questionType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(questionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionTypeExists(questionType.Id))
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
            return View(questionType);
        }

        // GET: QuestionTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.QuestionTypes == null)
            {
                return NotFound();
            }

            var questionType = await _context.QuestionTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (questionType == null)
            {
                return NotFound();
            }

            return View(questionType);
        }

        // POST: QuestionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.QuestionTypes == null)
            {
                return Problem("Entity set 'AppDbContext.QuestionTypes'  is null.");
            }
            var questionType = await _context.QuestionTypes.FindAsync(id);
            if (questionType != null)
            {
                _context.QuestionTypes.Remove(questionType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionTypeExists(Guid id)
        {
          return (_context.QuestionTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
