using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class ProfileViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FavoriteBook { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
        public List<OrderHistoryViewModels> OrderHistory { get; set; }
        public List<WhishlistViewModel> Whishlist { get; set; }
    }
}