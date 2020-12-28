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
    public class PersonelController : Controller
    {
        private readonly HastanaDbContext _context;

        public PersonelController(HastanaDbContext context)
        {
            _context = context;
        }

        // GET: Personel
        public async Task<IActionResult> Index()
        {
            var hastanaDbContext = _context.Personel.Include(p => p.hastaneKlinik).Include(p => p.kisi);
            return View(await hastanaDbContext.ToListAsync());
        }

        // GET: Personel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personel = await _context.Personel
                .Include(p => p.hastaneKlinik)
                .Include(p => p.kisi)
                .FirstOrDefaultAsync(m => m.id == id);
            if (personel == null)
            {
                return NotFound();
            }

            return View(personel);
        }

        // GET: Personel/Create
        public IActionResult Create()
        {
            var hastaneKlinik = _context.HastaneKlinik.Include(x => x.klinik).Include(x => x.hastane).Select(x => new { id = x.id, mekan = x.hastane.adi + " - " + x.klinik.adi }).ToList();
            ViewData["hastaneKlinikId"] = new SelectList(hastaneKlinik, "id", "mekan");
            var personeller = _context.Kisi.Where(x => x.kisiTuru == 'P' || x.kisiTuru == 'p').Select(x => new { id = x.id, tamIsim = x.tamIsim }).ToList();
            ViewData["id"] = new SelectList(personeller, "id", "tamIsim");
            return View();
        }

        // POST: Personel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,pozisyon,hastaneKlinikId")] Personel personel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["hastaneKlinikId"] = new SelectList(_context.HastaneKlinik, "id", "id", personel.hastaneKlinikId);
            ViewData["id"] = new SelectList(_context.Kisi, "id", "id", personel.id);
            return View(personel);
        }

        // GET: Personel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personel = await _context.Personel.FindAsync(id);
            if (personel == null)
            {
                return NotFound();
            }
            ViewData["hastaneKlinikId"] = new SelectList(_context.HastaneKlinik, "id", "id", personel.hastaneKlinikId);
            ViewData["id"] = new SelectList(_context.Kisi, "id", "id", personel.id);
            return View(personel);
        }

        // POST: Personel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,pozisyon,hastaneKlinikId")] Personel personel)
        {
            if (id != personel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonelExists(personel.id))
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
            ViewData["hastaneKlinikId"] = new SelectList(_context.HastaneKlinik, "id", "id", personel.hastaneKlinikId);
            ViewData["id"] = new SelectList(_context.Kisi, "id", "id", personel.id);
            return View(personel);
        }

        // GET: Personel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personel = await _context.Personel
                .Include(p => p.hastaneKlinik)
                .Include(p => p.kisi)
                .FirstOrDefaultAsync(m => m.id == id);
            if (personel == null)
            {
                return NotFound();
            }

            return View(personel);
        }

        // POST: Personel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personel = await _context.Personel.FindAsync(id);
            _context.Personel.Remove(personel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonelExists(int id)
        {
            return _context.Personel.Any(e => e.id == id);
        }
    }
}
