using System;

namespace BookCave.Models.InputModels
{
    public class ReviewInputModel
    {
        public int BookId { get; set; }
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        
        public string UserId { get; set; }
    }
}