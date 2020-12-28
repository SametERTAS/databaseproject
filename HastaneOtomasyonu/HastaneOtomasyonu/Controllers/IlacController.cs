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
    public class IlacController : Controller
    {
        private readonly HastanaDbContext _context;

        public IlacController(HastanaDbContext context)
        {
            _context = context;
        }

        // GET: Ilac
        public async Task<IActionResult> Index()
        {
            return View(await _context.Ilac.ToListAsync());
        }

        // GET: Ilac/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilac = await _context.Ilac
                .FirstOrDefaultAsync(m => m.ilacKodu == id);
            if (ilac == null)
            {
                return NotFound();
            }

            return View(ilac);
        }

        // GET: Ilac/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ilac/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ilacKodu,ilacAdi,ilacMarkasi,fiyat")] Ilac ilac)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ilac);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ilac);
        }

        // GET: Ilac/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilac = await _context.Ilac.FindAsync(id);
            if (ilac == null)
            {
                return NotFound();
            }
            return View(ilac);
        }

        // POST: Ilac/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ilacKodu,ilacAdi,ilacMarkasi,fiyat")] Ilac ilac)
        {
            if (id != ilac.ilacKodu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ilac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IlacExists(ilac.ilacKodu))
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
            return View(ilac);
        }

        // GET: Ilac/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ilac = await _context.Ilac
                .FirstOrDefaultAsync(m => m.ilacKodu == id);
            if (ilac == null)
            {
                return NotFound();
            }

            return View(ilac);
        }

        // POST: Ilac/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var ilac = await _context.Ilac.FindAsync(id);
            _context.Ilac.Remove(ilac);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IlacExists(string id)
        {
            return _context.Ilac.Any(e => e.ilacKodu == id);
        }
    }
}
