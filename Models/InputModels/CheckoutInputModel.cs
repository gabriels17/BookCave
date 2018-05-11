using System;
using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class CheckoutInputModel
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
         public string City { get; set; }
        public string State { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public string Country { get; set; }
        [Required(ErrorMessage ="Card number field is required")]
        public string CardNumber { get; set;}
        [Required(ErrorMessage ="Expire month is required")]
        public string ExpMonth { get; set; }
        [Required(ErrorMessage ="Expire year field is required")]
        public string ExpYear { get; set; }
        [Required(ErrorMessage ="Security code is required")]
        public string SecurityCode { get; set; }

    }
}