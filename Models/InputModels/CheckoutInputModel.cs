using System;
using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class CheckoutInputModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage ="Full Name is required!")]
        public string FullName { get; set; }
        [Required(ErrorMessage ="Shipping Address is required!")]
        public string ShippingAddress { get; set; }
        [Required(ErrorMessage ="City is required!")]
         public string City { get; set; }
        public string State { get; set; }
        [Required(ErrorMessage ="PostCode is required!")]
        public string PostCode { get; set; }
        [Required(ErrorMessage = "Country is required!")]
        public string Country { get; set; }

        public string CardNumber { get; set;}
        public string ExpMonth { get; set; }

        public string ExpYear { get; set; }
        
        public string SecurityCode { get; set; }

    }
}