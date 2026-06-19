using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2502E_Comicsys.Data;
using T2502E_Comicsys.ViewModels;

namespace T2502E_Comicsys.Controllers
{
    public class ReportsController : Controller
    {
        private readonly AppDbContext _context;

        public ReportsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Reports/RentalReport
        public async Task<IActionResult> RentalReport(DateTime? startDate, DateTime? endDate)
        {
            var model = new ReportViewModel
            {
                StartDate = startDate,
                EndDate = endDate
            };

            // Khởi tạo truy vấn cơ sở dữ liệu và nạp kèm thông tin khách hàng công khai
            var query = _context.Rentals.Include(r => r.Customer).AsQueryable();

            // Áp dụng điều kiện lọc theo Từ Ngày nếu có nhập
            if (startDate.HasValue)
            {
                query = query.Where(r => r.RentalDate >= startDate.Value.Date);
            }

            // Áp dụng điều kiện lọc theo Đến Ngày nếu có nhập
            if (endDate.HasValue)
            {
                // Thêm .AddDays(1) để bao gồm trọn vẹn cả ngày cuối cùng được chọn
                query = query.Where(r => r.RentalDate < endDate.Value.Date.AddDays(1));
            }

            // Sắp xếp đơn thuê mới nhất lên đầu và gán vào danh sách kết quả
            model.RentalsList = await query.OrderByDescending(r => r.RentalDate).ToListAsync();

            return View(model);
        }
    }
}