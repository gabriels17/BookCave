namespace BookCave.Data.EntityModels
{
    public class Account
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsStaff { get; set; }
    }
}