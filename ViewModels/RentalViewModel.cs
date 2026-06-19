using Microsoft.AspNetCore.Mvc.Rendering;

namespace T2502E_Comicsys.ViewModels;

public class RentalViewModel
{
    // Dữ liệu người dùng nhập
    public int CustomerId { get; set; }
    public int ComicBookId { get; set; }
    public DateTime RentalDate { get; set; } = DateTime.Now;
    public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(7);
    public int Quantity { get; set; }

    // Dữ liệu cho Dropdown list
    public IEnumerable<SelectListItem> CustomerList { get; set; }
    public IEnumerable<SelectListItem> ComicBookList { get; set; }
}