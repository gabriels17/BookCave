using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.ViewModels
{
    public class ProfileViewModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Name { get; set; }
    }
}