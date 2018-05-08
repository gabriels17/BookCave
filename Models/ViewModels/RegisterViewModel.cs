using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Email required!")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage ="First name required!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="Last name required!")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Password required!")]
        public string Password { get; set; }
    }
}