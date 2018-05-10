namespace BookCave.Data.EntityModels
{
    public class Review
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
    }
}