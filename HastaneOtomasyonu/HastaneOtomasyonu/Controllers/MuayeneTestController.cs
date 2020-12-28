using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HastaneOtomasyonu.Database;
using HastaneOtomasyonu.Models;

namespace HastaneOtomasyonu.Controllers
{
    public class MuayeneTestController : Controller
    {
        private readonly HastanaDbContext _context;

        public MuayeneTestController(HastanaDbContext context)
        {
            _context = context;
        }

        // GET: MuayeneTest
        public async Task<IActionResult> Index()
        {
            var hastanaDbContext = _context.MuayeneTest.Include(m => m.muayene).Include(m => m.test);
            return View(await hastanaDbContext.ToListAsync());
        }

        // GET: MuayeneTest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muayeneTest = await _context.MuayeneTest
                .Include(m => m.muayene)
                .Include(m => m.test)
                .FirstOrDefaultAsync(m => m.id == id);
            if (muayeneTest == null)
            {
                return NotFound();
            }

            return View(muayeneTest);
        }

        // GET: MuayeneTest/Create
        public IActionResult Create()
        {
            ViewData["muayeneId"] = new SelectList(_context.Muayene, "id", "id");
            ViewData["testId"] = new SelectList(_context.Test, "id", "id");
            return View();
        }

        // POST: MuayeneTest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,muayeneId,testId")] MuayeneTest muayeneTest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(muayeneTest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["muayeneId"] = new SelectList(_context.Muayene, "id", "id", muayeneTest.muayeneId);
            ViewData["testId"] = new SelectList(_context.Test, "id", "id", muayeneTest.testId);
            return View(muayeneTest);
        }

        // GET: MuayeneTest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muayeneTest = await _context.MuayeneTest.FindAsync(id);
            if (muayeneTest == null)
            {
                return NotFound();
            }
            ViewData["muayeneId"] = new SelectList(_context.Muayene, "id", "id", muayeneTest.muayeneId);
            ViewData["testId"] = new SelectList(_context.Test, "id", "id", muayeneTest.testId);
            return View(muayeneTest);
        }

        // POST: MuayeneTest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,muayeneId,testId")] MuayeneTest muayeneTest)
        {
            if (id != muayeneTest.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(muayeneTest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MuayeneTestExists(muayeneTest.id))
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
            ViewData["muayeneId"] = new SelectList(_context.Muayene, "id", "id", muayeneTest.muayeneId);
            ViewData["testId"] = new SelectList(_context.Test, "id", "id", muayeneTest.testId);
            return View(muayeneTest);
        }

        // GET: MuayeneTest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muayeneTest = await _context.MuayeneTest
                .Include(m => m.muayene)
                .Include(m => m.test)
                .FirstOrDefaultAsync(m => m.id == id);
            if (muayeneTest == null)
            {
                return NotFound();
            }

            return View(muayeneTest);
        }

        // POST: MuayeneTest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var muayeneTest = await _context.MuayeneTest.FindAsync(id);
            _context.MuayeneTest.Remove(muayeneTest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MuayeneTestExists(int id)
        {
            return _context.MuayeneTest.Any(e => e.id == id);
        }
    }
}
