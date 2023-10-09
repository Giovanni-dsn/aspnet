using Microsoft.EntityFrameworkCore;
using ApiModels;

namespace Data {
    public class AppDbContext : DbContext {
        public DbSet<ProductModel> Products { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<ProductModel>().Property( a => a.Name).HasMaxLength(50).IsRequired();
        //     modelBuilder.Entity<ProductModel>().Property( a => a.Code).HasMaxLength(20).IsRequired();
        // }
}
}
