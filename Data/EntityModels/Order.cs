namespace BookCave.Data.EntityModels
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int BookId { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public int ZipCode { get; set; }
        public double Price { get; set; }
    }
}