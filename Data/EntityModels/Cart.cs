namespace BookCave.Data.EntityModels
{
    public class Cart
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}