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
    public class KisiController : Controller
    {
        private readonly HastanaDbContext _context;

        public KisiController(HastanaDbContext context)
        {
            _context = context;
        }

        // GET: Kisi
        public async Task<IActionResult> Index()
        {
            var hastanaDbContext = _context.Kisi.Include(k => k.kanGrubu).Include(k => k.sehir);
            return View(await hastanaDbContext.ToListAsync());
        }

        // GET: Kisi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisi
                .Include(k => k.kanGrubu)
                .Include(k => k.sehir)
                .FirstOrDefaultAsync(m => m.id == id);
            if (kisi == null)
            {
                return NotFound();
            }

            return View(kisi);
        }

        // GET: Kisi/Create
        public IActionResult Create()
        {
            ViewData["kanGrubuId"] = new SelectList(_context.KanGrubu, "id", "adi");
            ViewData["sehirId"] = new SelectList(_context.Sehir, "id", "adi");
            return View();
        }

        // POST: Kisi/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,adi,soyadi,adres,cepNo,evNo,isNo,TCNo,dogumTarihi,cinsiyet,medeniDurum,sehirId,kanGrubuId,kisiTuru")] Kisi kisi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kisi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["kanGrubuId"] = new SelectList(_context.KanGrubu, "id", "id", kisi.kanGrubuId);
            ViewData["sehirId"] = new SelectList(_context.Sehir, "id", "adi", kisi.sehirId);
            return View(kisi);
        }

        // GET: Kisi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisi.FindAsync(id);
            if (kisi == null)
            {
                return NotFound();
            }
            ViewData["kanGrubuId"] = new SelectList(_context.KanGrubu, "id", "id", kisi.kanGrubuId);
            ViewData["sehirId"] = new SelectList(_context.Sehir, "id", "adi", kisi.sehirId);
            return View(kisi);
        }

        // POST: Kisi/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,adi,soyadi,adres,cepNo,evNo,isNo,TCNo,dogumTarihi,cinsiyet,medeniDurum,sehirId,kanGrubuId,kisiTuru")] Kisi kisi)
        {
            if (id != kisi.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kisi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KisiExists(kisi.id))
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
            ViewData["kanGrubuId"] = new SelectList(_context.KanGrubu, "id", "id", kisi.kanGrubuId);
            ViewData["sehirId"] = new SelectList(_context.Sehir, "id", "adi", kisi.sehirId);
            return View(kisi);
        }

        // GET: Kisi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kisi = await _context.Kisi
                .Include(k => k.kanGrubu)
                .Include(k => k.sehir)
                .FirstOrDefaultAsync(m => m.id == id);
            if (kisi == null)
            {
                return NotFound();
            }

            return View(kisi);
        }

        // POST: Kisi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kisi = await _context.Kisi.FindAsync(id);
            _context.Kisi.Remove(kisi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KisiExists(int id)
        {
            return _context.Kisi.Any(e => e.id == id);
        }
    }
}
