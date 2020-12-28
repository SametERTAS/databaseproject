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
    public class MuayeneController : Controller
    {
        private readonly HastanaDbContext _context;

        public MuayeneController(HastanaDbContext context)
        {
            _context = context;
        }

        // GET: Muayene
        public async Task<IActionResult> Index()
        {
            var hastanaDbContext = _context.Muayene.Include(m => m.randevu);
            return View(await hastanaDbContext.ToListAsync());
        }

        // GET: Muayene/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muayene = await _context.Muayene
                .Include(m => m.randevu)
                .FirstOrDefaultAsync(m => m.id == id);
            if (muayene == null)
            {
                return NotFound();
            }

            return View(muayene);
        }

        // GET: Muayene/Create
        public IActionResult Create()
        {
            var hastalar = _context.Randevu
                .Include(x => x.hasta).ThenInclude(x => x.kisi)
                .Include(x => x.doktor).ThenInclude(x => x.kisi)
                .Include(x => x.hastaneKlinik).ThenInclude(x => x.klinik)
                .Include(x => x.hastaneKlinik).ThenInclude(x => x.hastane)
                .Select(x => new { id = x.id, isim = x.hasta.kisi.tamIsim + "-" + x.hastaneKlinik.klinik.adi +"-"+ x.saat }).ToList();
            ViewData["id"] = new SelectList(hastalar, "id", "isim");
            return View();
        }

        // POST: Muayene/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,tani")] Muayene muayene)
        {
            if (ModelState.IsValid)
            {
                _context.Add(muayene);
                await _context.SaveChangesAsync();
               
            }
           
            return RedirectToAction("Index");
        }

        // GET: Muayene/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muayene = await _context.Muayene.FindAsync(id);
            if (muayene == null)
            {
                return NotFound();
            }
            ViewData["id"] = new SelectList(_context.Randevu, "id", "id", muayene.id);
            return View(muayene);
        }

        // POST: Muayene/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,tani")] Muayene muayene)
        {
            if (id != muayene.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(muayene);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MuayeneExists(muayene.id))
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
            ViewData["id"] = new SelectList(_context.Randevu, "id", "id", muayene.id);
            return View(muayene);
        }

        // GET: Muayene/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var muayene = await _context.Muayene
                .Include(m => m.randevu)
                .FirstOrDefaultAsync(m => m.id == id);
            if (muayene == null)
            {
                return NotFound();
            }

            return View(muayene);
        }

        // POST: Muayene/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var muayene = await _context.Muayene.FindAsync(id);
            _context.Muayene.Remove(muayene);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MuayeneExists(int id)
        {
            return _context.Muayene.Any(e => e.id == id);
        }
    }
}
