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
    public class KlinikController : Controller
    {
        private readonly HastanaDbContext _context;

        public KlinikController(HastanaDbContext context)
        {
            _context = context;
        }

        // GET: Klinik
        public async Task<IActionResult> Index()
        {
            return View(await _context.Klinik.ToListAsync());
        }

        // GET: Klinik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klinik = await _context.Klinik
                .FirstOrDefaultAsync(m => m.id == id);
            if (klinik == null)
            {
                return NotFound();
            }

            return View(klinik);
        }

        // GET: Klinik/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Klinik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,adi")] Klinik klinik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klinik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(klinik);
        }

        // GET: Klinik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klinik = await _context.Klinik.FindAsync(id);
            if (klinik == null)
            {
                return NotFound();
            }
            return View(klinik);
        }

        // POST: Klinik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,adi")] Klinik klinik)
        {
            if (id != klinik.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klinik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlinikExists(klinik.id))
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
            return View(klinik);
        }

        // GET: Klinik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klinik = await _context.Klinik
                .FirstOrDefaultAsync(m => m.id == id);
            if (klinik == null)
            {
                return NotFound();
            }

            return View(klinik);
        }

        // POST: Klinik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var klinik = await _context.Klinik.FindAsync(id);
            _context.Klinik.Remove(klinik);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlinikExists(int id)
        {
            return _context.Klinik.Any(e => e.id == id);
        }
    }
}
