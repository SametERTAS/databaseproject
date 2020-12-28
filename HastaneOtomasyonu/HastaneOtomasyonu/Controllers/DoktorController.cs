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
    public class DoktorController : Controller
    {
        private readonly HastanaDbContext _context;

        public DoktorController(HastanaDbContext context)
        {
            _context = context;
        }

        // GET: Doktor
        public async Task<IActionResult> Index()
        {
            var hastanaDbContext = _context.Doktor.Include(d => d.hastaneKlinik).Include(d => d.kisi);
            return View(await hastanaDbContext.ToListAsync());
        }

        // GET: Doktor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktor
                .Include(d => d.hastaneKlinik)
                .Include(d => d.kisi)
                .FirstOrDefaultAsync(m => m.id == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // GET: Doktor/Create
        public IActionResult Create()
        {
            var hastaneKlinik = _context.HastaneKlinik.Include(x => x.klinik).Include(x => x.hastane).Select(x => new { id = x.id, mekan = x.hastane.adi +" - "+ x.klinik.adi }).ToList();
            ViewData["hastaneKlinikId"] = new SelectList(hastaneKlinik, "id", "mekan");
         
            var doktorlar = _context.Kisi.Where(x => x.kisiTuru == 'D' || x.kisiTuru == 'd').Select(x => new { id = x.id, tamIsim = x.tamIsim }).ToList();
            ViewData["id"] = new SelectList(doktorlar, "id", "tamIsim");
            return View();
        }

        // POST: Doktor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,unvan,hastaneKlinikId")] Doktor doktor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doktor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["hastaneKlinikId"] = new SelectList(_context.HastaneKlinik, "id", "id", doktor.hastaneKlinikId);
            ViewData["id"] = new SelectList(_context.Kisi, "id", "id", doktor.id);
            return View(doktor);
        }

        // GET: Doktor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktor.FindAsync(id);
            if (doktor == null)
            {
                return NotFound();
            }
            ViewData["hastaneKlinikId"] = new SelectList(_context.HastaneKlinik, "id", "id", doktor.hastaneKlinikId);
            ViewData["id"] = new SelectList(_context.Kisi, "id", "id", doktor.id);
            return View(doktor);
        }

        // POST: Doktor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,unvan,hastaneKlinikId")] Doktor doktor)
        {
            if (id != doktor.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doktor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoktorExists(doktor.id))
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
            ViewData["hastaneKlinikId"] = new SelectList(_context.HastaneKlinik, "id", "id", doktor.hastaneKlinikId);
            ViewData["id"] = new SelectList(_context.Kisi, "id", "id", doktor.id);
            return View(doktor);
        }

        // GET: Doktor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktor
                .Include(d => d.hastaneKlinik)
                .Include(d => d.kisi)
                .FirstOrDefaultAsync(m => m.id == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // POST: Doktor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doktor = await _context.Doktor.FindAsync(id);
            _context.Doktor.Remove(doktor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoktorExists(int id)
        {
            return _context.Doktor.Any(e => e.id == id);
        }
    }
}
