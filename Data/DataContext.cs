using BookCave.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace BookCave.Data
{
    public class DataContext : DbContext
    {
        private const string ConnectionString = "Server=tcp:group17.database.windows.net,1433;Initial Catalog=BookCave;Persist Security Info=False;User ID=group17;Password=Admin170;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public DbSet<Book> Books { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Review> Reviews { get; set; }
        
        public DbSet<Order> Orders { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(ConnectionString);
        }
    }
}