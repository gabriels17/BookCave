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
        public string FavoriteBook { get; set; }
        public string Image { get; set; }
    }
}