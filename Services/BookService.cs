using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class BookService
    {
        private BookRepo _bookRepo;

        public BookService()
        {
            _bookRepo = new BookRepo();
        }

        public List<BookListViewModel> GetAllBooks()
        {
            var books = _bookRepo.GetAllBooks();
            return books; 
        }

        public HomeViewModel GetHomePage()
        {
            var books = new HomeViewModel { NewReleases = _bookRepo.GetNewReleases(), TopRated = _bookRepo.GetTopRated()};
            return books;
        }
    }
}