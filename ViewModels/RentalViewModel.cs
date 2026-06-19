using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace T2502E_Comicsys.ViewModels;

public class RentalViewModel
{
    // Dữ liệu người dùng nhập cho bảng Rentals
    [Required(ErrorMessage = "Vui lòng chọn khách hàng.")]
    public int CustomerId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập ngày thuê.")]
    [DataType(DataType.Date)]
    public DateTime RentalDate { get; set; } = DateTime.Now;

    [Required(ErrorMessage = "Vui lòng nhập ngày trả.")]
    [DataType(DataType.Date)]
    public DateTime ReturnDate { get; set; } = DateTime.Now.AddDays(7);

    [Required(ErrorMessage = "Vui lòng nhập trạng thái đơn thuê.")]
    public string Status { get; set; } = "Chưa trả";

    // Dữ liệu người dùng nhập cho bảng RentalDetails
    [Required(ErrorMessage = "Vui lòng chọn truyện.")]
    public int ComicBookId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải tối thiểu là 1.")]
    public int Quantity { get; set; } = 1;

    [Range(0, double.MaxValue, ErrorMessage = "Giá mỗi ngày phải là một giá trị dương.")]
    public decimal PricePerDay { get; set; }

    // Dữ liệu cho Dropdown list hiển thị trên giao diện View
    public IEnumerable<SelectListItem>? CustomerList { get; set; }
    public IEnumerable<SelectListItem>? ComicBookList { get; set; }
}