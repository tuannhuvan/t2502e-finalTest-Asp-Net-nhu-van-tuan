using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T2502E_Comicsys.Data;
using T2502E_Comicsys.Models;

namespace T2502E_Comicsys.Controllers
{
    public class RentalDetailsController : Controller
    {
        private readonly AppDbContext _context;

        public RentalDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: RentalDetails
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.RentalDetails.Include(r => r.ComicBook).Include(r => r.Rental);
            return View(await appDbContext.ToListAsync());
        }

        // GET: RentalDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalDetails = await _context.RentalDetails
                .Include(r => r.ComicBook)
                .Include(r => r.Rental)
                .FirstOrDefaultAsync(m => m.RentalDetailId == id);
            if (rentalDetails == null)
            {
                return NotFound();
            }

            return View(rentalDetails);
        }

        // GET: RentalDetails/Create
        public IActionResult Create()
        {
            ViewData["ComicBookId"] = new SelectList(_context.ComicBooks, "ComicBookId", "ComicBookId");
            ViewData["RentalId"] = new SelectList(_context.Rentals, "RentalID", "RentalID");
            return View();
        }

        // POST: RentalDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RentalDetailId,RentalId,ComicBookId,Quantity,PricePerDay")] RentalDetails rentalDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rentalDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComicBookId"] = new SelectList(_context.ComicBooks, "ComicBookId", "ComicBookId", rentalDetails.ComicBookId);
            ViewData["RentalId"] = new SelectList(_context.Rentals, "RentalID", "RentalID", rentalDetails.RentalId);
            return View(rentalDetails);
        }

        // GET: RentalDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalDetails = await _context.RentalDetails.FindAsync(id);
            if (rentalDetails == null)
            {
                return NotFound();
            }
            ViewData["ComicBookId"] = new SelectList(_context.ComicBooks, "ComicBookId", "ComicBookId", rentalDetails.ComicBookId);
            ViewData["RentalId"] = new SelectList(_context.Rentals, "RentalID", "RentalID", rentalDetails.RentalId);
            return View(rentalDetails);
        }

        // POST: RentalDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalDetailId,RentalId,ComicBookId,Quantity,PricePerDay")] RentalDetails rentalDetails)
        {
            if (id != rentalDetails.RentalDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentalDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalDetailsExists(rentalDetails.RentalDetailId))
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
            ViewData["ComicBookId"] = new SelectList(_context.ComicBooks, "ComicBookId", "ComicBookId", rentalDetails.ComicBookId);
            ViewData["RentalId"] = new SelectList(_context.Rentals, "RentalID", "RentalID", rentalDetails.RentalId);
            return View(rentalDetails);
        }

        // GET: RentalDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentalDetails = await _context.RentalDetails
                .Include(r => r.ComicBook)
                .Include(r => r.Rental)
                .FirstOrDefaultAsync(m => m.RentalDetailId == id);
            if (rentalDetails == null)
            {
                return NotFound();
            }

            return View(rentalDetails);
        }

        // POST: RentalDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentalDetails = await _context.RentalDetails.FindAsync(id);
            if (rentalDetails != null)
            {
                _context.RentalDetails.Remove(rentalDetails);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalDetailsExists(int id)
        {
            return _context.RentalDetails.Any(e => e.RentalDetailId == id);
        }
    }
}
