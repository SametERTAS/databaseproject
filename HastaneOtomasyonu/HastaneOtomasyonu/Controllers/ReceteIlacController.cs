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
    public class ReceteIlacController : Controller
    {
        private readonly HastanaDbContext _context;

        public ReceteIlacController(HastanaDbContext context)
        {
            _context = context;
        }

        // GET: ReceteIlac
        public async Task<IActionResult> Index()
        {
            var hastanaDbContext = _context.ReceteIlac.Include(r => r.ilac).Include(r => r.recete);
            return View(await hastanaDbContext.ToListAsync());
        }

        // GET: ReceteIlac/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receteIlac = await _context.ReceteIlac
                .Include(r => r.ilac)
                .Include(r => r.recete)
                .FirstOrDefaultAsync(m => m.id == id);
            if (receteIlac == null)
            {
                return NotFound();
            }

            return View(receteIlac);
        }

        // GET: ReceteIlac/Create
        public IActionResult Create()
        {
            ViewData["ilacKodu"] = new SelectList(_context.Ilac, "ilacKodu", "ilacKodu");
            var recete = _context.Recete.Include(x => x.muayene).ThenInclude(x => x.randevu).ThenInclude(x => x.hasta).ThenInclude(x => x.kisi).Select(x => new { receteNo = x.receteNo, kim = x.receteNo + " " + x.muayene.randevu.hasta.kisi.tamIsim }).ToList();

            ViewData["receteNo"] = new SelectList(recete, "receteNo", "kim");
            return View();
        }

        // POST: ReceteIlac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,receteNo,ilacKodu")] ReceteIlac receteIlac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(receteIlac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ilacKodu"] = new SelectList(_context.Ilac, "ilacKodu", "ilacKodu", receteIlac.ilacKodu);
            ViewData["receteNo"] = new SelectList(_context.Recete, "receteNo", "receteNo", receteIlac.receteNo);
            return View(receteIlac);
        }

        // GET: ReceteIlac/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receteIlac = await _context.ReceteIlac.FindAsync(id);
            if (receteIlac == null)
            {
                return NotFound();
            }
            ViewData["ilacKodu"] = new SelectList(_context.Ilac, "ilacKodu", "ilacKodu", receteIlac.ilacKodu);
            ViewData["receteNo"] = new SelectList(_context.Recete, "receteNo", "receteNo", receteIlac.receteNo);
            return View(receteIlac);
        }

        // POST: ReceteIlac/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,receteNo,ilacKodu")] ReceteIlac receteIlac)
        {
            if (id != receteIlac.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(receteIlac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceteIlacExists(receteIlac.id))
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
            ViewData["ilacKodu"] = new SelectList(_context.Ilac, "ilacKodu", "ilacKodu", receteIlac.ilacKodu);
            ViewData["receteNo"] = new SelectList(_context.Recete, "receteNo", "receteNo", receteIlac.receteNo);
            return View(receteIlac);
        }

        // GET: ReceteIlac/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receteIlac = await _context.ReceteIlac
                .Include(r => r.ilac)
                .Include(r => r.recete)
                .FirstOrDefaultAsync(m => m.id == id);
            if (receteIlac == null)
            {
                return NotFound();
            }

            return View(receteIlac);
        }

        // POST: ReceteIlac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receteIlac = await _context.ReceteIlac.FindAsync(id);
            _context.ReceteIlac.Remove(receteIlac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceteIlacExists(int id)
        {
            return _context.ReceteIlac.Any(e => e.id == id);
        }
    }
}
