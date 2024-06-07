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
    public class ScenesController : Controller
    {
        private readonly AppDbContext _context;

        public ScenesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Scenes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Scenes.Include(s => s.Chapter).Include(s => s.Emotion).Include(s => s.Image).Include(s => s.SceneType).Include(s => s.Speaker);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Scenes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Scenes == null)
            {
                return NotFound();
            }

            var scene = await _context.Scenes
                .Include(s => s.Chapter)
                .Include(s => s.Emotion)
                .Include(s => s.Image)
                .Include(s => s.SceneType)
                .Include(s => s.Speaker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scene == null)
            {
                return NotFound();
            }

            return View(scene);
        }

        // GET: Scenes/Create
        public IActionResult Create()
        {
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Name");
            ViewData["EmotionId"] = new SelectList(_context.Emotions, "Id", "Name");
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Name");
            ViewData["SceneTypeId"] = new SelectList(_context.SceneTypes, "Id", "Name");
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "Id", "Name");
            return View();
        }

        // POST: Scenes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content,SceneTypeId,EmotionId,Number,SpeakerId,ChapterId,ImageId,Id")] Scene scene)
        {
            if (ModelState.IsValid)
            {
                scene.Id = Guid.NewGuid();
                _context.Add(scene);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Name", scene.ChapterId);
            ViewData["EmotionId"] = new SelectList(_context.Emotions, "Id", "Name", scene.EmotionId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Name", scene.ImageId);
            ViewData["SceneTypeId"] = new SelectList(_context.SceneTypes, "Id", "Name", scene.SceneTypeId);
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "Id", "Name", scene.SpeakerId);
            return View(scene);
        }

        // GET: Scenes/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Scenes == null)
            {
                return NotFound();
            }

            var scene = await _context.Scenes.FindAsync(id);
            if (scene == null)
            {
                return NotFound();
            }
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Name", scene.ChapterId);
            ViewData["EmotionId"] = new SelectList(_context.Emotions, "Id", "Name", scene.EmotionId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Name", scene.ImageId);
            ViewData["SceneTypeId"] = new SelectList(_context.SceneTypes, "Id", "Name", scene.SceneTypeId);
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "Id", "Name", scene.SpeakerId);
            return View(scene);
        }

        // POST: Scenes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Content,SceneTypeId,EmotionId,Number,SpeakerId,ChapterId,ImageId,Id")] Scene scene)
        {
            if (id != scene.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scene);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SceneExists(scene.Id))
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
            ViewData["ChapterId"] = new SelectList(_context.Chapters, "Id", "Name", scene.ChapterId);
            ViewData["EmotionId"] = new SelectList(_context.Emotions, "Id", "Name", scene.EmotionId);
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Name", scene.ImageId);
            ViewData["SceneTypeId"] = new SelectList(_context.SceneTypes, "Id", "Name", scene.SceneTypeId);
            ViewData["SpeakerId"] = new SelectList(_context.Speakers, "Id", "Name", scene.SpeakerId);
            return View(scene);
        }

        // GET: Scenes/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Scenes == null)
            {
                return NotFound();
            }

            var scene = await _context.Scenes
                .Include(s => s.Chapter)
                .Include(s => s.Emotion)
                .Include(s => s.Image)
                .Include(s => s.SceneType)
                .Include(s => s.Speaker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scene == null)
            {
                return NotFound();
            }

            return View(scene);
        }

        // POST: Scenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Scenes == null)
            {
                return Problem("Entity set 'AppDbContext.Scenes'  is null.");
            }
            var scene = await _context.Scenes.FindAsync(id);
            if (scene != null)
            {
                _context.Scenes.Remove(scene);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SceneExists(Guid id)
        {
          return (_context.Scenes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
