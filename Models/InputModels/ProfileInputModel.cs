using System;
using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class ProfileInputModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage ="First name is required!")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Last name is required!")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FavoriteBook { get; set; }
        public string Image { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string Country { get; set; }
    }
}