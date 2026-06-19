using Microsoft.EntityFrameworkCore;
using T2502E_Comicsys.Models; 

namespace T2502E_Comicsys.Data
{
    public class ComicSystemContext(DbContextOptions<ComicSystemContext> options) : DbContext(options)
    {
        // Định nghĩa các DbSet cho các bảng trong database
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ComicBook?> ComicBooks { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<RentalDetail> RentalDetails { get; set; }

        // Cấu hình thêm (nếu cần, ví dụ như thiết lập quan hệ phức tạp)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Ví dụ: Đảm bảo bảng RentalDetails có cấu trúc khóa chính đúng
            modelBuilder.Entity<RentalDetail>()
                .HasKey(rd => rd.RentalDetailId);
        }
    }
}