using Microsoft.AspNetCore.Identity;

namespace BookCave.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FavoriteBook { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public string UserRole { get; set; }
    }
}