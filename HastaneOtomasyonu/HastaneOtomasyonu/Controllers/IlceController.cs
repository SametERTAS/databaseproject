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
    public class IlceController : Controller
    {
        private readonly HastanaDbContext _context;

        public IlceController(HastanaDbContext context)
        {
            _context = context;
        }

        // GET: Ilce
        public async Task<IActionResult> Index()
        {
            var hastanaDbContext = _context.Ilce.Include(i => i.sehir);
            return View(await hastanaDbContext.ToListAsync());
        }

        // GET: Ilce/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilce = await _context.Ilce
                .Include(i => i.sehir)
                .FirstOrDefaultAsync(m => m.id == id);
            if (ilce == null)
            {
                return NotFound();
            }

            return View(ilce);
        }

        // GET: Ilce/Create
        public IActionResult Create()
        {
            ViewData["sehirId"] = new SelectList(_context.Sehir, "id", "adi");
            return View();
        }

        // POST: Ilce/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,adi,telefonKodu,sehirId")] Ilce ilce)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ilce);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["sehirId"] = new SelectList(_context.Sehir, "id", "adi", ilce.sehirId);
            return View(ilce);
        }

        // GET: Ilce/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilce = await _context.Ilce.FindAsync(id);
            if (ilce == null)
            {
                return NotFound();
            }
            ViewData["sehirId"] = new SelectList(_context.Sehir, "id", "adi", ilce.sehirId);
            return View(ilce);
        }

        // POST: Ilce/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,adi,telefonKodu,sehirId")] Ilce ilce)
        {
            if (id != ilce.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ilce);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IlceExists(ilce.id))
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
            ViewData["sehirId"] = new SelectList(_context.Sehir, "id", "adi", ilce.sehirId);
            return View(ilce);
        }

        // GET: Ilce/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilce = await _context.Ilce
                .Include(i => i.sehir)
                .FirstOrDefaultAsync(m => m.id == id);
            if (ilce == null)
            {
                return NotFound();
            }

            return View(ilce);
        }

        // POST: Ilce/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ilce = await _context.Ilce.FindAsync(id);
            _context.Ilce.Remove(ilce);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IlceExists(int id)
        {
            return _context.Ilce.Any(e => e.id == id);
        }
    }
}
