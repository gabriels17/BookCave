using System.Collections.Generic;

namespace BookCave.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<BookListViewModel> NewReleases { get; set; }
        public List<BookListViewModel> TopRated { get; set; }     
    }
}