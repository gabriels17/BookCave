using System;
using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class CartBoughtViewModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string ShippingAddress { get; set; }
         public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        public string CardNumber { get; set;}
        public string ExpMonth { get; set; }
        public string ExpYear { get; set; }        
        public string SecurityCode { get; set; }

    }
}