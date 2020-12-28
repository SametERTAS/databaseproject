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
    public class ReceteController : Controller
    {
        private readonly HastanaDbContext _context;

        public ReceteController(HastanaDbContext context)
        {
            _context = context;
        }

        // GET: Recete
        public async Task<IActionResult> Index()
        {
            var hastanaDbContext = _context.Recete.Include(r => r.muayene);
            return View(await hastanaDbContext.ToListAsync());
        }

        // GET: Recete/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recete = await _context.Recete
                .Include(r => r.muayene)
                .FirstOrDefaultAsync(m => m.receteNo == id);
            if (recete == null)
            {
                return NotFound();
            }

            return View(recete);
        }

        // GET: Recete/Create
        public IActionResult Create()
        {
            var kisi = _context.Muayene.Include(x => x.randevu).ThenInclude(x => x.hasta).ThenInclude(x => x.kisi).Select(x => new { id = x.id, tamIsim = x.randevu.hasta.kisi.tamIsim + " " + x.randevu.saat }).ToList();
            ViewData["muayeneId"] = new SelectList(kisi, "id", "tamIsim");
            return View();
        }

        // POST: Recete/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("receteNo,tarih,muayeneId")] Recete recete)
        {
            if (ModelState.IsValid)
            {
                _context.Add(recete);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["muayeneId"] = new SelectList(_context.Muayene, "id", "id", recete.muayeneId);
            return View(recete);
        }

        // GET: Recete/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recete = await _context.Recete.FindAsync(id);
            if (recete == null)
            {
                return NotFound();
            }
            ViewData["muayeneId"] = new SelectList(_context.Muayene, "id", "id", recete.muayeneId);
            return View(recete);
        }

        // POST: Recete/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("receteNo,tarih,muayeneId")] Recete recete)
        {
            if (id != recete.receteNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(recete);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceteExists(recete.receteNo))
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
            ViewData["muayeneId"] = new SelectList(_context.Muayene, "id", "id", recete.muayeneId);
            return View(recete);
        }

        // GET: Recete/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recete = await _context.Recete
                .Include(r => r.muayene)
                .FirstOrDefaultAsync(m => m.receteNo == id);
            if (recete == null)
            {
                return NotFound();
            }

            return View(recete);
        }

        // POST: Recete/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var recete = await _context.Recete.FindAsync(id);
            _context.Recete.Remove(recete);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceteExists(string id)
        {
            return _context.Recete.Any(e => e.receteNo == id);
        }
    }
}
