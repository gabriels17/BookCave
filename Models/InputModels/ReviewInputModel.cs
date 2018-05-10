using System;
using System.ComponentModel.DataAnnotations;

namespace BookCave.Models.InputModels
{
    public class ReviewInputModel
    {
        public int BookId { get; set; }

        [Range(0, 5, ErrorMessage ="A rating must be left with all reviews")]
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
    }
}