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
    public class ImageTypesController : Controller
    {
        private readonly AppDbContext _context;

        public ImageTypesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ImageTypes
        public async Task<IActionResult> Index()
        {
              return _context.ImageTypes != null ? 
                          View(await _context.ImageTypes.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.ImageTypes'  is null.");
        }

        // GET: ImageTypes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ImageTypes == null)
            {
                return NotFound();
            }

            var imageType = await _context.ImageTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageType == null)
            {
                return NotFound();
            }

            return View(imageType);
        }

        // GET: ImageTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ImageTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id")] ImageType imageType)
        {
            if (ModelState.IsValid)
            {
                imageType.Id = Guid.NewGuid();
                _context.Add(imageType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(imageType);
        }

        // GET: ImageTypes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ImageTypes == null)
            {
                return NotFound();
            }

            var imageType = await _context.ImageTypes.FindAsync(id);
            if (imageType == null)
            {
                return NotFound();
            }
            return View(imageType);
        }

        // POST: ImageTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Id")] ImageType imageType)
        {
            if (id != imageType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(imageType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImageTypeExists(imageType.Id))
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
            return View(imageType);
        }

        // GET: ImageTypes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ImageTypes == null)
            {
                return NotFound();
            }

            var imageType = await _context.ImageTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (imageType == null)
            {
                return NotFound();
            }

            return View(imageType);
        }

        // POST: ImageTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ImageTypes == null)
            {
                return Problem("Entity set 'AppDbContext.ImageTypes'  is null.");
            }
            var imageType = await _context.ImageTypes.FindAsync(id);
            if (imageType != null)
            {
                _context.ImageTypes.Remove(imageType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImageTypeExists(Guid id)
        {
          return (_context.ImageTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
