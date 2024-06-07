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
    public class SceneTypesController : Controller
    {
        private readonly AppDbContext _context;

        public SceneTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: SceneTypes
        public async Task<IActionResult> Index()
        {
              return _context.SceneTypes != null ? 
                          View(await _context.SceneTypes.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.SceneTypes'  is null.");
        }

        // GET: SceneTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.SceneTypes == null)
            {
                return NotFound();
            }

            var sceneType = await _context.SceneTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sceneType == null)
            {
                return NotFound();
            }

            return View(sceneType);
        }

        // GET: SceneTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SceneTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] SceneType sceneType)
        {
            if (ModelState.IsValid)
            {
                sceneType.Id = Guid.NewGuid();
                _context.Add(sceneType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sceneType);
        }

        // GET: SceneTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.SceneTypes == null)
            {
                return NotFound();
            }

            var sceneType = await _context.SceneTypes.FindAsync(id);
            if (sceneType == null)
            {
                return NotFound();
            }
            return View(sceneType);
        }

        // POST: SceneTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] SceneType sceneType)
        {
            if (id != sceneType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sceneType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SceneTypeExists(sceneType.Id))
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
            return View(sceneType);
        }

        // GET: SceneTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.SceneTypes == null)
            {
                return NotFound();
            }

            var sceneType = await _context.SceneTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sceneType == null)
            {
                return NotFound();
            }

            return View(sceneType);
        }

        // POST: SceneTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.SceneTypes == null)
            {
                return Problem("Entity set 'AppDbContext.SceneTypes'  is null.");
            }
            var sceneType = await _context.SceneTypes.FindAsync(id);
            if (sceneType != null)
            {
                _context.SceneTypes.Remove(sceneType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SceneTypeExists(Guid id)
        {
          return (_context.SceneTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
