namespace BookCave.Data.EntityModels
{
    public class PaymentInfo
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
    }
}