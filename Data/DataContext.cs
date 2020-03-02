using BookCave.Data.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace BookCave.Data
{
    public class DataContext : DbContext
    {
        private const string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=Banani567;Database=BookCave";

        public DbSet<Book> Books { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<Review> Reviews { get; set; }
        
        public DbSet<Order> Orders { get; set; }

        public DbSet<Wishlist> Wishlists { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }
}