using Microsoft.EntityFrameworkCore;
using T2502E_Comicsys.Models;

namespace T2502E_Comicsys.Data

{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        // Định nghĩa các DbSet cho các bảng trong database

        public DbSet<Customers> Customers { get; set; }
        public DbSet<ComicBooks> ComicBooks { get; set; }
        public DbSet<Rentals> Rentals { get; set; }
        public DbSet<T2502E_Comicsys.Models.RentalDetails> RentalDetails { get; set; } = null!;

        // Cấu hình thêm (nếu cần, ví dụ như thiết lập quan hệ phức tạp)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Ví dụ: Đảm bảo bảng RentalDetails có cấu trúc khóa chính đúng
            modelBuilder.Entity<RentalDetails>()
                .HasKey(rd => rd.RentalDetailId);
        }
    }
}