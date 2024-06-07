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
    public class StoryResultsController : Controller
    {
        private readonly AppDbContext _context;

        public StoryResultsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: StoryResults
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.StoryResults.Include(s => s.Story).Include(s => s.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: StoryResults/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.StoryResults == null)
            {
                return NotFound();
            }

            var storyResult = await _context.StoryResults
                .Include(s => s.Story)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storyResult == null)
            {
                return NotFound();
            }

            return View(storyResult);
        }

        // GET: StoryResults/Create
        public IActionResult Create()
        {
            ViewData["StoryId"] = new SelectList(_context.Stories, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: StoryResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StoryId,MoralCount,UserId,Id")] StoryResult storyResult)
        {
            if (ModelState.IsValid)
            {
                storyResult.Id = Guid.NewGuid();
                _context.Add(storyResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StoryId"] = new SelectList(_context.Stories, "Id", "Description", storyResult.StoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", storyResult.UserId);
            return View(storyResult);
        }

        // GET: StoryResults/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.StoryResults == null)
            {
                return NotFound();
            }

            var storyResult = await _context.StoryResults.FindAsync(id);
            if (storyResult == null)
            {
                return NotFound();
            }
            ViewData["StoryId"] = new SelectList(_context.Stories, "Id", "Description", storyResult.StoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", storyResult.UserId);
            return View(storyResult);
        }

        // POST: StoryResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StoryId,MoralCount,UserId,Id")] StoryResult storyResult)
        {
            if (id != storyResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storyResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoryResultExists(storyResult.Id))
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
            ViewData["StoryId"] = new SelectList(_context.Stories, "Id", "Description", storyResult.StoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", storyResult.UserId);
            return View(storyResult);
        }

        // GET: StoryResults/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.StoryResults == null)
            {
                return NotFound();
            }

            var storyResult = await _context.StoryResults
                .Include(s => s.Story)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storyResult == null)
            {
                return NotFound();
            }

            return View(storyResult);
        }

        // POST: StoryResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.StoryResults == null)
            {
                return Problem("Entity set 'AppDbContext.StoryResults'  is null.");
            }
            var storyResult = await _context.StoryResults.FindAsync(id);
            if (storyResult != null)
            {
                _context.StoryResults.Remove(storyResult);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoryResultExists(Guid id)
        {
          return (_context.StoryResults?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
