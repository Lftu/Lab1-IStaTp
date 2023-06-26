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
    public class RealtyOwnersController : Controller
    {
        private readonly RealEstateDbContext _context;

        public RealtyOwnersController(RealEstateDbContext context)
        {
            _context = context;
        }

        // GET: RealtyOwners
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Realties");
            }
            ViewBag.Id = id;
            var realEstateDbContext = _context.RealtyOwners.Where(r => r.RealtyId == id).Include(r => r.Owner);
            return View(await realEstateDbContext.ToListAsync());
        }

        // GET: RealtyOwners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RealtyOwners == null)
            {
                return NotFound();
            }

            var realtyOwner = await _context.RealtyOwners
                .Include(r => r.Owner)
                .Include(r => r.Realty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realtyOwner == null)
            {
                return NotFound();
            }

            return View(realtyOwner);
        }

        // GET: RealtyOwners/Create
        public IActionResult Create(int id)
        {
            ViewBag.RealtyId = id;
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Name");
            return View();
        }

        // POST: RealtyOwners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int RealtyId, [Bind("OwnerId,StartDate,EndDate")] RealtyOwner realtyOwner)
        {
            realtyOwner.RealtyId = RealtyId;
            if (ModelState.IsValid)
            {
                _context.Add(realtyOwner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = RealtyId });
            }
            ViewBag.Id = RealtyId;
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id", realtyOwner.OwnerId);
            return View(realtyOwner);
        }

        // GET: RealtyOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RealtyOwners == null)
            {
                return NotFound();
            }

            var realtyOwner = await _context.RealtyOwners.FindAsync(id);
            if (realtyOwner == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id", realtyOwner.OwnerId);
            ViewData["RealtyId"] = new SelectList(_context.Realties, "Id", "Id", realtyOwner.RealtyId);
            return View(realtyOwner);
        }

        // POST: RealtyOwners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RealtyId,OwnerId,StartDate,EndDate")] RealtyOwner realtyOwner)
        {
            if (id != realtyOwner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(realtyOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RealtyOwnerExists(realtyOwner.Id))
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
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id", realtyOwner.OwnerId);
            ViewData["RealtyId"] = new SelectList(_context.Realties, "Id", "Id", realtyOwner.RealtyId);
            return View(realtyOwner);
        }

        // GET: RealtyOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RealtyOwners == null)
            {
                return NotFound();
            }

            var realtyOwner = await _context.RealtyOwners
                .Include(r => r.Owner)
                .Include(r => r.Realty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realtyOwner == null)
            {
                return NotFound();
            }

            return View(realtyOwner);
        }

        // POST: RealtyOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RealtyOwners == null)
            {
                return Problem("Entity set 'RealEstateDbContext.RealtyOwners'  is null.");
            }
            var realtyOwner = await _context.RealtyOwners.FindAsync(id);
            if (realtyOwner != null)
            {
                _context.RealtyOwners.Remove(realtyOwner);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RealtyOwnerExists(int id)
        {
          return (_context.RealtyOwners?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
