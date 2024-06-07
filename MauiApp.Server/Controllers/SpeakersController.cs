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
    public class SpeakersController : Controller
    {
        private readonly AppDbContext _context;

        public SpeakersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Speakers
        public async Task<IActionResult> Index()
        {
              return _context.Speakers != null ? 
                          View(await _context.Speakers.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Speakers'  is null.");
        }

        // GET: Speakers/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Speakers == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speakers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // GET: Speakers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Speakers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] Speaker speaker)
        {
            if (ModelState.IsValid)
            {
                speaker.Id = Guid.NewGuid();
                _context.Add(speaker);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(speaker);
        }

        // GET: Speakers/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Speakers == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speakers.FindAsync(id);
            if (speaker == null)
            {
                return NotFound();
            }
            return View(speaker);
        }

        // POST: Speakers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] Speaker speaker)
        {
            if (id != speaker.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speaker);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpeakerExists(speaker.Id))
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
            return View(speaker);
        }

        // GET: Speakers/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Speakers == null)
            {
                return NotFound();
            }

            var speaker = await _context.Speakers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (speaker == null)
            {
                return NotFound();
            }

            return View(speaker);
        }

        // POST: Speakers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Speakers == null)
            {
                return Problem("Entity set 'AppDbContext.Speakers'  is null.");
            }
            var speaker = await _context.Speakers.FindAsync(id);
            if (speaker != null)
            {
                _context.Speakers.Remove(speaker);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpeakerExists(Guid id)
        {
          return (_context.Speakers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
