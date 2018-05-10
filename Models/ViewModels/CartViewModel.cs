namespace BookCave.Models.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public double Rating { get; set; }
        public string Genre { get; internal set; }
        public int Quantity { get; set; }
    }
}