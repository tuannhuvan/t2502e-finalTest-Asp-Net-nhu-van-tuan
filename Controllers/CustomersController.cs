using Microsoft.AspNetCore.Mvc;
using T2502E_Comicsys.Data;
using T2502E_Comicsys.Models;

namespace T2502E_Comicsys.Controllers
{
    public class CustomersController(ComicSystemContext context) : Controller
    {
        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,PhoneNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                // Tự động gán ngày đăng ký là ngày hiện tại
                customer.RegisterDate = DateTime.Now;
                
                context.Add(customer);
                await context.SaveChangesAsync();
                
                // Sau khi đăng ký thành công, thường sẽ chuyển về trang danh sách hoặc thông báo
                return RedirectToAction("Index", "Home"); 
            }
            return View(customer);
        }
    }
}