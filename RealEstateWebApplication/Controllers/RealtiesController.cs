using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RealEstateWebApplication.Models;

namespace RealEstateWebApplication.Controllers
{
    public class RealtiesController : Controller
    {
        private readonly RealEstateDbContext _context;

        public RealtiesController(RealEstateDbContext context)
        {
            _context = context;
        }

        // GET: Realties
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null || name == null)
            {
                return RedirectToAction("Index", "Types");
            }
            ViewBag.TypeId = id;
            ViewBag.TypeName = name;
            var realEstateDbContext = _context.Realties.Where(r => r.TypeId == id);
            return View(await realEstateDbContext.ToListAsync());
        }

        // GET: Realties/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Realties == null)
            {
                return NotFound();
            }

            var realty = await _context.Realties
                .Include(r => r.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realty == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "RealtyOwners", new { id = realty.Id });
        }

        // GET: Realties/Create
        public IActionResult Create(int id)
        {
            ViewBag.TypeId = id;
            ViewBag.TypeName = _context.Types.First(t => t.Id == id).Name;
            return View();
        }

        // POST: Realties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int TypeId, [Bind("Area,FloorNumber,RoomsNumber,Price")] Realty realty)
        {
            realty.TypeId = TypeId;
            if (ModelState.IsValid)
            {
                _context.Add(realty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = TypeId, name = _context.Types.First(t => t.Id == TypeId).Name });
            }
            ViewBag.TypeId = realty.TypeId;
            ViewBag.TypeName = _context.Types.First(t => t.Id == realty.TypeId).Name;
            return View(realty);
        }

        // GET: Realties/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Realties == null)
            {
                return NotFound();
            }

            var realty = await _context.Realties.FindAsync(id);
            if (realty == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Id", realty.TypeId);
            return View(realty);
        }

        // POST: Realties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeId,Area,FloorNumber,RoomsNumber,Price")] Realty realty)
        {
            if (id != realty.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(realty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RealtyExists(realty.Id))
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
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Id", realty.TypeId);
            return View(realty);
        }

        // GET: Realties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Realties == null)
            {
                return NotFound();
            }

            var realty = await _context.Realties
                .Include(r => r.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realty == null)
            {
                return NotFound();
            }

            return View(realty);
        }

        // POST: Realties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Realties == null)
            {
                return Problem("Entity set 'RealEstateDbContext.Realties'  is null.");
            }
            var realty = await _context.Realties.FindAsync(id);
            if (realty != null)
            {
                _context.Realties.Remove(realty);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RealtyExists(int id)
        {
          return (_context.Realties?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
