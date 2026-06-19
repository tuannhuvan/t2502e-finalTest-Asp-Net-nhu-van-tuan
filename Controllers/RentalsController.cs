using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using T2502E_Comicsys.Data;
using T2502E_Comicsys.Models;
using T2502E_Comicsys.ViewModels;

namespace T2502E_Comicsys.Controllers;

public class RentalsController(ComicSystemContext context) : Controller
{
    // GET: Tạo trang thuê
    public IActionResult Create()
    {
        var model = new RentalViewModel
        {
            CustomerList = context.Customers.Select(c => new SelectListItem { Value = c.CustomerId.ToString(), Text = c.FullName }),
            ComicBookList = context.ComicBooks.Select(b => new SelectListItem { Value = b.ComicBookId.ToString(), Text = b.Title })
        };
        return View(model);
    }

    // POST: Lưu thông tin thuê
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RentalViewModel model)
    {
        if (ModelState.IsValid)
        {
            // 1. Tạo Rental
            var rental = new Rental {
                CustomerId = model.CustomerId,
                RentalDate = model.RentalDate,
                ReturnDate = model.ReturnDate,
                Status = "Đang thuê"
            };
            context.Rentals.Add(rental);
            await context.SaveChangesAsync(); // Cần save để có RentalId

            // 2. Lấy giá sách để tính tiền
            var book = await context.ComicBooks.FindAsync(model.ComicBookId);
            
            // 3. Tạo RentalDetail
            var detail = new RentalDetail {
                RentalId = rental.RentalId,
                ComicBookId = model.ComicBookId,
                Quantity = model.Quantity,
                Price = book.PricePerDay * model.Quantity // Logic tính giá đơn giản
            };
            context.RentalDetails.Add(detail);
            await context.SaveChangesAsync();

            return RedirectToAction("Index"); // Chuyển về trang danh sách thuê
        }
        return View(model);
    }
}