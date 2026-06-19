using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace T2502E_Comicsys.Models;

public class ComicBook
{
    [Key]
    public int ComicBookId { get; set; }

    [Required, StringLength(255)]
    public string Title { get; set; }

    [Required, StringLength(255)]
    public string Author { get; set; }

    [Required, Column(TypeName = "decimal(10, 2)")]
    public decimal PricePerDay { get; set; }

    // Navigation property
    public ICollection<RentalDetail> RentalDetails { get; set; }
}