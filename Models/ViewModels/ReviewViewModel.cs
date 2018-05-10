using System;

namespace BookCave.Models.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public double Rating { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
    }
}