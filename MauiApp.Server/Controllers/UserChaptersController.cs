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
    public class UserChaptersController : Controller
    {
        private readonly AppDbContext _context;

        public UserChaptersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserChapters
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserChapters.Include(u => u.Chapter).Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserChapters/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.UserChapters == null)
            {
                return NotFound();
            }

            var userChapter = await _context.UserChapters
                .Include(u => u.Chapter)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userChapter == null)
            {
                return NotFound();
            }

            return View(userChapter);
        }

        // GET: UserChapters/Create
        public IActionResult Create()
        {
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        // POST: UserChapters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,ChapterId,IsActive,Id")] UserChapter userChapter)
        {
            if (ModelState.IsValid)
            {
                userChapter.Id = Guid.NewGuid();
                _context.Add(userChapter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Name", userChapter.ChapterId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userChapter.UserId);
            return View(userChapter);
        }

        // GET: UserChapters/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.UserChapters == null)
            {
                return NotFound();
            }

            var userChapter = await _context.UserChapters.FindAsync(id);
            if (userChapter == null)
            {
                return NotFound();
            }
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Name", userChapter.ChapterId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userChapter.UserId);
            return View(userChapter);
        }

        // POST: UserChapters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,ChapterId,IsActive,Id")] UserChapter userChapter)
        {
            if (id != userChapter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userChapter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserChapterExists(userChapter.Id))
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
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Name", userChapter.ChapterId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", userChapter.UserId);
            return View(userChapter);
        }

        // GET: UserChapters/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.UserChapters == null)
            {
                return NotFound();
            }

            var userChapter = await _context.UserChapters
                .Include(u => u.Chapter)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userChapter == null)
            {
                return NotFound();
            }

            return View(userChapter);
        }

        // POST: UserChapters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.UserChapters == null)
            {
                return Problem("Entity set 'AppDbContext.UserChapters'  is null.");
            }
            var userChapter = await _context.UserChapters.FindAsync(id);
            if (userChapter != null)
            {
                _context.UserChapters.Remove(userChapter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserChapterExists(Guid id)
        {
          return (_context.UserChapters?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
