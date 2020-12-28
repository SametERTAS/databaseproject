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
    public class SehirController : Controller
    {
        private readonly HastanaDbContext _context;

        public SehirController(HastanaDbContext context)
        {
            _context = context;
        }

        // GET: Sehir
        public async Task<IActionResult> Index()
        {
            var hastanaDbContext = _context.Sehir.Include(s => s.ulke);
            return View(await hastanaDbContext.ToListAsync());
        }

        // GET: Sehir/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sehir = await _context.Sehir
                .Include(s => s.ulke)
                .FirstOrDefaultAsync(m => m.id == id);
            if (sehir == null)
            {
                return NotFound();
            }

            return View(sehir);
        }

        // GET: Sehir/Create
        public IActionResult Create()
        {
            ViewData["ulkeId"] = new SelectList(_context.Ulke, "id", "adi");
            return View();
        }

        // POST: Sehir/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,adi,plakaKodu,telefonKodu,ulkeId")] Sehir sehir)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sehir);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ulkeId"] = new SelectList(_context.Ulke, "id", "adi", sehir.ulkeId);
            return View(sehir);
        }

        // GET: Sehir/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sehir = await _context.Sehir.FindAsync(id);
            if (sehir == null)
            {
                return NotFound();
            }
            ViewData["ulkeId"] = new SelectList(_context.Ulke, "id", "adi", sehir.ulkeId);
            return View(sehir);
        }

        // POST: Sehir/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,adi,plakaKodu,telefonKodu,ulkeId")] Sehir sehir)
        {
            if (id != sehir.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sehir);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SehirExists(sehir.id))
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
            ViewData["ulkeId"] = new SelectList(_context.Ulke, "id", "adi", sehir.ulkeId);
            return View(sehir);
        }

        // GET: Sehir/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sehir = await _context.Sehir
                .Include(s => s.ulke)
                .FirstOrDefaultAsync(m => m.id == id);
            if (sehir == null)
            {
                return NotFound();
            }

            return View(sehir);
        }

        // POST: Sehir/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sehir = await _context.Sehir.FindAsync(id);
            _context.Sehir.Remove(sehir);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SehirExists(int id)
        {
            return _context.Sehir.Any(e => e.id == id);
        }
    }
}
