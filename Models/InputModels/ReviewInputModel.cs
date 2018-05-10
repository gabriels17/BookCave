using System;
using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class ReviewInputModel
    {
        public int BookId { get; set; }

        [Range(1, 5, ErrorMessage ="Rating must be between 1 and 5")]
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
    }
}