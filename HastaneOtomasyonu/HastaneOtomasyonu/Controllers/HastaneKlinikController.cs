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
    public class HastaneKlinikController : Controller
    {
        private readonly HastanaDbContext _context;

        public HastaneKlinikController(HastanaDbContext context)
        {
            _context = context;
        }

        // GET: HastaneKlinik
        public async Task<IActionResult> Index()
        {
            var hastanaDbContext = _context.HastaneKlinik.Include(h => h.hastane).Include(h => h.klinik);
            return View(await hastanaDbContext.ToListAsync());
        }

        // GET: HastaneKlinik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastaneKlinik = await _context.HastaneKlinik
                .Include(h => h.hastane)
                .Include(h => h.klinik)
                .FirstOrDefaultAsync(m => m.id == id);
            if (hastaneKlinik == null)
            {
                return NotFound();
            }

            return View(hastaneKlinik);
        }

        // GET: HastaneKlinik/Create
        public IActionResult Create()
        {
            ViewData["hastaneId"] = new SelectList(_context.Hastane, "id", "adi");
            ViewData["klinikId"] = new SelectList(_context.Klinik, "id", "adi");
            return View();
        }

        // POST: HastaneKlinik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,hastaneId,klinikId")] HastaneKlinik hastaneKlinik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hastaneKlinik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["hastaneId"] = new SelectList(_context.Hastane, "id", "adi", hastaneKlinik.hastaneId);
            ViewData["klinikId"] = new SelectList(_context.Klinik, "id", "adi", hastaneKlinik.klinikId);
            return View(hastaneKlinik);
        }

        // GET: HastaneKlinik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastaneKlinik = await _context.HastaneKlinik.FindAsync(id);
            if (hastaneKlinik == null)
            {
                return NotFound();
            }
            ViewData["hastaneId"] = new SelectList(_context.Hastane, "id", "adi", hastaneKlinik.hastaneId);
            ViewData["klinikId"] = new SelectList(_context.Klinik, "id", "adi", hastaneKlinik.klinikId);
            return View(hastaneKlinik);
        }

        // POST: HastaneKlinik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,hastaneId,klinikId")] HastaneKlinik hastaneKlinik)
        {
            if (id != hastaneKlinik.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hastaneKlinik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastaneKlinikExists(hastaneKlinik.id))
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
            ViewData["hastaneId"] = new SelectList(_context.Hastane, "id", "adi", hastaneKlinik.hastaneId);
            ViewData["klinikId"] = new SelectList(_context.Klinik, "id", "adi", hastaneKlinik.klinikId);
            return View(hastaneKlinik);
        }

        // GET: HastaneKlinik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hastaneKlinik = await _context.HastaneKlinik
                .Include(h => h.hastane)
                .Include(h => h.klinik)
                .FirstOrDefaultAsync(m => m.id == id);
            if (hastaneKlinik == null)
            {
                return NotFound();
            }

            return View(hastaneKlinik);
        }

        // POST: HastaneKlinik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hastaneKlinik = await _context.HastaneKlinik.FindAsync(id);
            _context.HastaneKlinik.Remove(hastaneKlinik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HastaneKlinikExists(int id)
        {
            return _context.HastaneKlinik.Any(e => e.id == id);
        }
    }
}
