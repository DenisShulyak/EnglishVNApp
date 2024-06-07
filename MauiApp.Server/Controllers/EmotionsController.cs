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
    public class EmotionsController : Controller
    {
        private readonly AppDbContext _context;

        public EmotionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Emotions
        public async Task<IActionResult> Index()
        {
              return _context.Emotions != null ? 
                          View(await _context.Emotions.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Emotions'  is null.");
        }

        // GET: Emotions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Emotions == null)
            {
                return NotFound();
            }

            var emotion = await _context.Emotions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emotion == null)
            {
                return NotFound();
            }

            return View(emotion);
        }

        // GET: Emotions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Emotions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] Emotion emotion)
        {
            if (ModelState.IsValid)
            {
                emotion.Id = Guid.NewGuid();
                _context.Add(emotion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(emotion);
        }

        // GET: Emotions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Emotions == null)
            {
                return NotFound();
            }

            var emotion = await _context.Emotions.FindAsync(id);
            if (emotion == null)
            {
                return NotFound();
            }
            return View(emotion);
        }

        // POST: Emotions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] Emotion emotion)
        {
            if (id != emotion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(emotion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmotionExists(emotion.Id))
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
            return View(emotion);
        }

        // GET: Emotions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Emotions == null)
            {
                return NotFound();
            }

            var emotion = await _context.Emotions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (emotion == null)
            {
                return NotFound();
            }

            return View(emotion);
        }

        // POST: Emotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Emotions == null)
            {
                return Problem("Entity set 'AppDbContext.Emotions'  is null.");
            }
            var emotion = await _context.Emotions.FindAsync(id);
            if (emotion != null)
            {
                _context.Emotions.Remove(emotion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmotionExists(Guid id)
        {
          return (_context.Emotions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
