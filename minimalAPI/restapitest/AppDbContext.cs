using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext {
    public DbSet<ProductModel> Products { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductModel>().Property( a => a.Name).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<ProductModel>().Property( a => a.Code).HasMaxLength(20).IsRequired();
    }

}