namespace BookCave.Data.EntityModels
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string State { get; set; }

    }
}