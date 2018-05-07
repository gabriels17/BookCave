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

        public List<BookListViewModel> Search(string str)
        {
            var books = _bookRepo.GetAllBooks();
            
            var byname  = (from a in books
                        where a.Title.ToLower().Contains(str.ToLower())
                        select a);
            var byauthor = (from a in books
                        where a.Author.ToLower().Contains(str.ToLower())
                        select a);
            var result = byname.Concat(byauthor).ToList();      
            return result;
        }

        public void AddToDatabase(BookInputModel newBook)
        {
            _bookRepo.AddToDatabase(newBook);
        }
    }
}