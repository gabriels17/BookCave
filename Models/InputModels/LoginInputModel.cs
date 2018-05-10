using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class LoginInputModel
    {
        [Required(ErrorMessage ="Email is required!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password is required!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}