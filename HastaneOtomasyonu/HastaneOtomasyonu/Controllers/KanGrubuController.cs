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
    public class KanGrubuController : Controller
    {
        private readonly HastanaDbContext _context;

        public KanGrubuController(HastanaDbContext context)
        {
            _context = context;
        }

        // GET: KanGrubu
        public async Task<IActionResult> Index()
        {
            return View(await _context.KanGrubu.ToListAsync());
        }

        // GET: KanGrubu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kanGrubu = await _context.KanGrubu
                .FirstOrDefaultAsync(m => m.id == id);
            if (kanGrubu == null)
            {
                return NotFound();
            }

            return View(kanGrubu);
        }

        // GET: KanGrubu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KanGrubu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,adi")] KanGrubu kanGrubu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kanGrubu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kanGrubu);
        }

        // GET: KanGrubu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kanGrubu = await _context.KanGrubu.FindAsync(id);
            if (kanGrubu == null)
            {
                return NotFound();
            }
            return View(kanGrubu);
        }

        // POST: KanGrubu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,adi")] KanGrubu kanGrubu)
        {
            if (id != kanGrubu.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kanGrubu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KanGrubuExists(kanGrubu.id))
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
            return View(kanGrubu);
        }

        // GET: KanGrubu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kanGrubu = await _context.KanGrubu
                .FirstOrDefaultAsync(m => m.id == id);
            if (kanGrubu == null)
            {
                return NotFound();
            }

            return View(kanGrubu);
        }

        // POST: KanGrubu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kanGrubu = await _context.KanGrubu.FindAsync(id);
            _context.KanGrubu.Remove(kanGrubu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KanGrubuExists(int id)
        {
            return _context.KanGrubu.Any(e => e.id == id);
        }
    }
}
