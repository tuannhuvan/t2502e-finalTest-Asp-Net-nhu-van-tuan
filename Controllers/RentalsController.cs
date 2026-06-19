using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using T2502E_Comicsys.Data;
using T2502E_Comicsys.Models;
using T2502E_Comicsys.ViewModels;

namespace T2502E_Comicsys.Controllers
{
    public class RentalsController : Controller
    {
        private readonly AppDbContext _context;

        public RentalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            // Sử dụng .Include để nạp kèm thông tin Customer, giúp lấy được thuộc tính FullName
            var rentalsList = await _context.Rentals
                .Include(r => r.Customer)
                .ToListAsync();
                                   
            return View(rentalsList);
        }

        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentals = await _context.Rentals
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.RentalID == id);
            if (rentals == null)
            {
                return NotFound();
            }

            return View(rentals);
        }

        // GET: Rentals/Create
        public IActionResult Create()
        {
            var viewModel = new RentalViewModel
            {
                CustomerList = _context.Customers.Select(c => new SelectListItem
                {
                    Value = c.CustomerID.ToString(),
                    Text = c.FullName
                }).ToList(),
                ComicBookList = _context.ComicBooks.Select(b => new SelectListItem
                {
                    Value = b.ComicBookId.ToString(),
                    Text = b.Title
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: Rentals/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(T2502E_Comicsys.ViewModels.RentalViewModel model)
        {
            if (ModelState.IsValid)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    // 1. Thêm vào bảng Rentals
                    var rental = new Rentals
                    {
                        CustomerID = model.CustomerId,
                        RentalDate = model.RentalDate,
                        ReturnDate = model.ReturnDate,
                        Status = model.Status
                    };
                    _context.Rentals.Add(rental);
                    await _context.SaveChangesAsync(); 

                    // 2. Thêm vào bảng RentalDetails
                    var rentalDetail = new RentalDetails
                    {
                        RentalId = rental.RentalID, 
                        ComicBookId = model.ComicBookId,
                        Quantity = model.Quantity,
                        PricePerDay = model.PricePerDay
                    };
                    _context.RentalDetails.Add(rentalDetail);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    ModelState.AddModelError("", "Đã xảy ra lỗi hệ thống khi lưu hóa đơn. Vui lòng thử lại.");
                }
            }

            // Khi Form lỗi, nạp lại dữ liệu trực tiếp vào List của 'model' thay vì dùng ViewBag
            model.CustomerList = _context.Customers.Select(c => new SelectListItem
            {
                Value = c.CustomerID.ToString(),
                Text = c.FullName
            }).ToList();

            model.ComicBookList = _context.ComicBooks.Select(b => new SelectListItem
            {
                Value = b.ComicBookId.ToString(),
                Text = b.Title
            }).ToList();

            // Trả lại model đã được nạp đầy đủ 2 danh sách list để giao diện render lại không bị trống
            return View(model);
        }

        // GET: Rentals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentals = await _context.Rentals.FindAsync(id);
            if (rentals == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName", rentals.CustomerID);
            return View(rentals);
        }

        // POST: Rentals/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RentalID,CustomerID,RentalDate,ReturnDate,Status")] Rentals rentals)
        {
            if (id != rentals.RentalID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rentals);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalsExists(rentals.RentalID))
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
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FullName", rentals.CustomerID);
            return View(rentals);
        }

        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rentals = await _context.Rentals
                .Include(r => r.Customer)
                .FirstOrDefaultAsync(m => m.RentalID == id);
            if (rentals == null)
            {
                return NotFound();
            }

            return View(rentals);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rentals = await _context.Rentals.FindAsync(id);
            if (rentals != null)
            {
                _context.Rentals.Remove(rentals);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RentalsExists(int id)
        {
            return _context.Rentals.Any(e => e.RentalID == id);
        }
    }
}