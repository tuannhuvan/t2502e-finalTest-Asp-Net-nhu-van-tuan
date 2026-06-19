using T2502E_Comicsys.Models;

namespace T2502E_Comicsys.ViewModels;

public class ReportViewModel
{
    // Điều kiện lọc thời gian đầu vào
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Danh sách kết quả trả về sau khi lọc
    public List<Rentals> RentalsList { get; set; } = new List<Rentals>();
}