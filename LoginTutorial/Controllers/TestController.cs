using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LoginTutorial.Data;
using LoginTutorial.Models;

namespace LoginTutorial.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Test
        public async Task<IActionResult> Index()
        {
              return _context.TestEntities != null ? 
                          View(await _context.TestEntities.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TestEntities'  is null.");
        }

        // GET: Test/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TestEntities == null)
            {
                return NotFound();
            }

            var testEntity = await _context.TestEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testEntity == null)
            {
                return NotFound();
            }

            return View(testEntity);
        }

        // GET: Test/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Mobile,Email,Source")] TestEntity testEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(testEntity);
        }

        // GET: Test/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TestEntities == null)
            {
                return NotFound();
            }

            var testEntity = await _context.TestEntities.FindAsync(id);
            if (testEntity == null)
            {
                return NotFound();
            }
            return View(testEntity);
        }

        // POST: Test/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Mobile,Email,Source")] TestEntity testEntity)
        {
            if (id != testEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestEntityExists(testEntity.Id))
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
            return View(testEntity);
        }

        // GET: Test/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TestEntities == null)
            {
                return NotFound();
            }

            var testEntity = await _context.TestEntities
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testEntity == null)
            {
                return NotFound();
            }

            return View(testEntity);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TestEntities == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TestEntities'  is null.");
            }
            var testEntity = await _context.TestEntities.FindAsync(id);
            if (testEntity != null)
            {
                _context.TestEntities.Remove(testEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestEntityExists(int id)
        {
          return (_context.TestEntities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
