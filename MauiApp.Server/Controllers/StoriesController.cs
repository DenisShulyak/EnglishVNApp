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
    public class StoriesController : Controller
    {
        private readonly AppDbContext _context;

        public StoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Stories
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Stories.Include(s => s.Image);
            var test = await appDbContext.ToListAsync();
            test.AddRange(new List<Story>
            {
                new Story
                {
                    Id = Guid.NewGuid(),
                    Description = "На маленьком острове в Тихом океане раз в сто лет случается необычное природное явление – лунное затмение, которое наделяет жителей сверхъестественными способностями. Молодая девушка по имени Лейла случайно обнаруживает, что может управлять водой, и должна решить, использовать ли свои новые силы для добра или зла",
                    Image = new Image
                    {
                        Id = Guid.NewGuid(),
                        Name = "story2.jpg"
                    },
                    Name = "Сияние луны",
                    Number = 2
                },new Story
                {
                    Id = Guid.NewGuid(),
                    Description = "Древний ритуал, проводимый каждую полнолуние в заброшенном замке, пробуждает древнее зло. Группа студентов-археологов, исследующих руины, оказывается втянутой в борьбу с темными силами.",
                    Image = new Image
                    {
                        Id = Guid.NewGuid(),
                        Name = "story3.jpg"
                    },
                    Name = "Тень в ночи",
                    Number = 3
                }
                ,new Story
                {
                    Id = Guid.NewGuid(),
                    Description = "В небольшом прибрежном городке ночью начинают исчезать люди. Местный рыболов обнаруживает, что виной всему мифические существа – сирены, которые охотятся за душами. Он решает организовать команду смельчаков, чтобы противостоять угрозе.",
                    Image = new Image
                    {
                        Id = Guid.NewGuid(),
                        Name = "story4.jpg"
                    },
                    Name = "Песнь сирены",
                    Number = 4
                }
                ,new Story
                {
                    Id = Guid.NewGuid(),
                    Description = "В лесах северной Канады появляется загадочный зверь, который охотится только при лунном свете. Охотник Джек отправляется на поиски загадочного существа, чтобы спасти свой городок.",
                    Image = new Image
                    {
                        Id = Guid.NewGuid(),
                        Name = "story5.jpg"
                    },
                    Name = "Лунный зверь",
                    Number = 5
                }
                ,new Story
                {
                    Id = Guid.NewGuid(),
                    Description = "В средневековой деревне жители подозревают, что их соседка – ведьма, которая может управлять луной. Когда на деревню обрушивается серия несчастий, они решают провести суд и узнать правду.",
                    Image = new Image
                    {
                        Id = Guid.NewGuid(),
                        Name = "story6.jpg"
                    },
                    Name = "Заклинание ведьмы",
                    Number = 6
                }
                ,new Story
                {
                    Id = Guid.NewGuid(),
                    Description = "Старый маяк на скалистом побережье оказывается местом, где происходят странные явления во время полнолуния. Молодая исследовательница приезжает, чтобы разгадать тайну и сталкивается с призраками прошлого.",
                    Image = new Image
                    {
                        Id = Guid.NewGuid(),
                        Name = "story7.jpg"
                    },
                    Name = "Секреты старого маяка",
                    Number = 7
                }
            });
            return View(test);
        }

        // GET: Stories/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Stories == null)
            {
                return NotFound();
            }

            var story = await _context.Stories
                .Include(s => s.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (story == null)
            {
                return NotFound();
            }

            return View(story);
        }

        // GET: Stories/Create
        public IActionResult Create()
        {
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Name");
            return View();
        }

        // POST: Stories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Number,Description,ImageId,Id")] Story story)
        {
            if (ModelState.IsValid)
            {
                story.Id = Guid.NewGuid();
                _context.Add(story);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Name", story.ImageId);
            return View(story);
        }

        // GET: Stories/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Stories == null)
            {
                return NotFound();
            }

            var story = await _context.Stories.FindAsync(id);
            if (story == null)
            {
                return NotFound();
            }
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Name", story.ImageId);
            return View(story);
        }

        // POST: Stories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Name,Number,Description,ImageId,Id")] Story story)
        {
            if (id != story.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(story);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoryExists(story.Id))
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
            ViewData["ImageId"] = new SelectList(_context.Images, "Id", "Name", story.ImageId);
            return View(story);
        }

        // GET: Stories/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Stories == null)
            {
                return NotFound();
            }

            var story = await _context.Stories
                .Include(s => s.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (story == null)
            {
                return NotFound();
            }

            return View(story);
        }

        // POST: Stories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Stories == null)
            {
                return Problem("Entity set 'AppDbContext.Stories'  is null.");
            }
            var story = await _context.Stories.FindAsync(id);
            if (story != null)
            {
                _context.Stories.Remove(story);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StoryExists(Guid id)
        {
          return (_context.Stories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
