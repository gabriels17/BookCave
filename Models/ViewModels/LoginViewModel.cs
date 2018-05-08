using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Email required!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="Password required!")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }
}