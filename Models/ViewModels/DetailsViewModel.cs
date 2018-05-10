using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class DetailsViewModel
    {
        public BookDetailsViewModel Book {get; set; }
        public List<ReviewViewModel> Reviews { get; set; }     
    }
}