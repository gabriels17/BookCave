namespace BookCave.Models.ViewModels
{
    public class OrderHistoryViewModels
    {
        public string UserID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}