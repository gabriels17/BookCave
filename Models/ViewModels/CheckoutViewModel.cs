using System;
using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
    }
}