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
    public class RandevuController : Controller
    {
        private readonly HastanaDbContext _context;

        public RandevuController(HastanaDbContext context)
        {
            _context = context;
        }

        // GET: Randevu
        public async Task<IActionResult> Index()
        {
          
            var hastanaDbContext = _context.Randevu.Include(r => r.doktor).Include(r => r.hasta).Include(r => r.hastaneKlinik);
            return View(await hastanaDbContext.ToListAsync());
        }

        // GET: Randevu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu
                .Include(r => r.doktor)
                .Include(r => r.hasta)
                .Include(r => r.hastaneKlinik)
                .FirstOrDefaultAsync(m => m.id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // GET: Randevu/Create
        public IActionResult Create()
        {
            var doktorlar = _context.Doktor.Include(x=>x.kisi).Where(x => x.kisi.kisiTuru == 'D' || x.kisi.kisiTuru == 'd').Select(x => new { id = x.id, tamIsim = x.kisi.tamIsim }).ToList();
            ViewData["doktorId"] = new SelectList(doktorlar, "id", "tamIsim");
            var hastalar = _context.Hasta.Include(x => x.kisi).Where(x => x.kisi.kisiTuru == 'H' || x.kisi.kisiTuru == 'h').Select(x => new { id = x.id, tamIsim = x.kisi.tamIsim }).ToList();
            ViewData["hastaId"] = new SelectList(hastalar, "id", "tamIsim");
            ViewData["hastaneKlinikId"] = new SelectList(_context.HastaneKlinik, "id", "id");
            return View();
        }

        // POST: Randevu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,saat,hastaneKlinikId,hastaId,doktorId")] Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["doktorId"] = new SelectList(_context.Doktor, "id", "id", randevu.doktorId);
            ViewData["hastaId"] = new SelectList(_context.Hasta, "id", "id", randevu.hastaId);
            ViewData["hastaneKlinikId"] = new SelectList(_context.HastaneKlinik, "id", "id", randevu.hastaneKlinikId);
            return View(randevu);
        }

        // GET: Randevu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }
            ViewData["doktorId"] = new SelectList(_context.Doktor, "id", "id", randevu.doktorId);
            ViewData["hastaId"] = new SelectList(_context.Hasta, "id", "id", randevu.hastaId);
            ViewData["hastaneKlinikId"] = new SelectList(_context.HastaneKlinik, "id", "id", randevu.hastaneKlinikId);
            return View(randevu);
        }

        // POST: Randevu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,saat,hastaneKlinikId,hastaId,doktorId")] Randevu randevu)
        {
            if (id != randevu.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(randevu.id))
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
            ViewData["doktorId"] = new SelectList(_context.Doktor, "id", "id", randevu.doktorId);
            ViewData["hastaId"] = new SelectList(_context.Hasta, "id", "id", randevu.hastaId);
            ViewData["hastaneKlinikId"] = new SelectList(_context.HastaneKlinik, "id", "id", randevu.hastaneKlinikId);
            return View(randevu);
        }

        // GET: Randevu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevu
                .Include(r => r.doktor)
                .Include(r => r.hasta)
                .Include(r => r.hastaneKlinik)
                .FirstOrDefaultAsync(m => m.id == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // POST: Randevu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var randevu = await _context.Randevu.FindAsync(id);
            _context.Randevu.Remove(randevu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuExists(int id)
        {
            return _context.Randevu.Any(e => e.id == id);
        }
    }
}
