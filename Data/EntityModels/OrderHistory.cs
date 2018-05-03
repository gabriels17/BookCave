namespace BookCave.Data.EntityModels
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
    }
}