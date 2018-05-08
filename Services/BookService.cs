using System.Collections.Generic;
using System.Linq;
using BookCave.Models.InputModels;
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
        
        public List<BookListViewModel> Search(string str,List<BookListViewModel> books)
        {   
            var byname  = (from a in books
                        where a.Title.ToLower().Contains(str.ToLower())
                        select a);
            var byauthor = (from a in books
                        where a.Author.ToLower().Contains(str.ToLower())
                        select a);
            var result = byname.Concat(byauthor).ToList();      
            return result;
        }

        public BookDetailsViewModel GetBookById(int id)
        {
            var allbooks = _bookRepo.GetAllBooksDetails(id);
            // var idbook = (from b in allbooks
            // where b.Id == id
            // select b).SingleOrDefault();

            return allbooks; 
        }
        public void AddToDatabase(BookInputModel newBook)
        {
            _bookRepo.AddToDatabase(newBook);
        }
        public List<BookListViewModel> Filter(string str,List<BookListViewModel> Books)
        {
            var result = (from a in Books  
                        where a.Genre.ToLower() == str.ToLower()
                        select a).ToList();
            return result;
        }

        public List<BookListViewModel> SortByAz(List<BookListViewModel> books)
        {
            var sorted = (from a in books
                        orderby a.Title ascending
                        select a).ToList();

            return sorted;
        }

        public List<BookListViewModel> SortByZa(List<BookListViewModel> books)
        {
            var sorted = (from a in books
                        orderby a.Title descending
                        select a).ToList();

            return sorted;
        }

        public List<BookListViewModel> SortByRating(List<BookListViewModel> books)
        {
            var sorted = (from a in books
                        orderby a.Rating descending
                        select a).ToList();

            return sorted;
        }

        public List<BookListViewModel> SortByPriceHigh(List<BookListViewModel> books)
        {
            var sorted = (from a in books
                        orderby a.Price descending
                        select a).ToList();

            return sorted;
        }

        public List<BookListViewModel> SortByPriceLow(List<BookListViewModel> books)
        {
            var sorted = (from a in books
                        orderby a.Price ascending
                        select a).ToList();

            return sorted;
        }

        public List<BookListViewModel> SortByReleaseNewest(List<BookListViewModel> books)
        {
            var sorted = (from a in books
                        orderby a.ReleaseDate descending
                        select a).ToList();

            return sorted;
        }

        public List<BookListViewModel> SortByReleaseOldest(List<BookListViewModel> books)
        {
            var sorted = (from a in books
                        orderby a.ReleaseDate ascending
                        select a).ToList();

            return sorted;
        }
    }
}