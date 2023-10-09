using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Data {
    public class AppDbContext : DbContext {
        public DbSet<Book> Books {get; set;}
        
         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
         {
            
         }
        public DbSet<Author> Authors {get; set;}

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseSqlServer("Server=localhost;Database=Books;User Id=sa;Password=Sqlserversenha123;MultipleActiveResultSets=true;Encrypt=YES;TrustServerCertificate=YES");
        // }
    }
}