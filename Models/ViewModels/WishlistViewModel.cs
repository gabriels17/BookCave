namespace BookCave.Models.ViewModels
{
    public class WishlistViewModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
    }
}