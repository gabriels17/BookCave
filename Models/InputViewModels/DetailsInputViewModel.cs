using System.Collections.Generic;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;

namespace BookCave.Models.InputViewModels
{
    public class DetailsInputViewModel
    {
        public BookDetailsViewModel Book {get; set; }
        public List<ReviewViewModel> Reviews { get; set; } 
        public ReviewInputModel Input { get; set; }    
    }
}