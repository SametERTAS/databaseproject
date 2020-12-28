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
    public class HastaController : Controller
    {
        private readonly HastanaDbContext _context;

        public HastaController(HastanaDbContext context)
        {
            _context = context;
        }

        // GET: Hasta
        public async Task<IActionResult> Index()
        {
            var hastanaDbContext = _context.Hasta.Include(h => h.kisi);
            return View(await hastanaDbContext.ToListAsync());
        }

        // GET: Hasta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hasta
                .Include(h => h.kisi)
                .FirstOrDefaultAsync(m => m.id == id);
            if (hasta == null)
            {
                return NotFound();
            }

            return View(hasta);
        }

        // GET: Hasta/Create
        public IActionResult Create()
        {
            var hastalar = _context.Kisi.Where(x => x.kisiTuru == 'H' || x.kisiTuru == 'h').Select(x => new { id = x.id, tamIsim = x.tamIsim }).ToList();
            ViewData["id"] = new SelectList(hastalar, "id", "tamIsim");
            return View();
        }

        // POST: Hasta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id")] Hasta hasta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hasta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["id"] = new SelectList(_context.Kisi, "id", "id", hasta.id);
            return View(hasta);
        }

        // GET: Hasta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hasta.FindAsync(id);
            if (hasta == null)
            {
                return NotFound();
            }
         //   var hastalar = _context.Kisi.Where(x => x.kisiTuru == 'H' || x.kisiTuru == 'h').Select(x => new { id = x.id, tamIsim = x.tamIsim }).ToList();
            ViewData["id"] = new SelectList(_context.Kisi, "id", "id", hasta.id);
            return View(hasta);
        }

        // POST: Hasta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id")] Hasta hasta)
        {
            if (id != hasta.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hasta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HastaExists(hasta.id))
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
            ViewData["id"] = new SelectList(_context.Kisi, "id", "id", hasta.id);
            return View(hasta);
        }

        // GET: Hasta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hasta = await _context.Hasta
                .Include(h => h.kisi)
                .FirstOrDefaultAsync(m => m.id == id);
            if (hasta == null)
            {
                return NotFound();
            }

            return View(hasta);
        }

        // POST: Hasta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hasta = await _context.Hasta.FindAsync(id);
            _context.Hasta.Remove(hasta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HastaExists(int id)
        {
            return _context.Hasta.Any(e => e.id == id);
        }
    }
}
