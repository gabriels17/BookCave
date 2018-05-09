using System.ComponentModel.DataAnnotations;

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
    }
}