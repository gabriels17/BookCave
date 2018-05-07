using System.Collections.Generic;
using System.Linq;
using BookCave.Data;
using BookCave.Models.ViewModels;

namespace BookCave.Repositories
{
    public class BookRepo
    {
        private DataContext _db;

        public BookRepo()
        {
            _db = new DataContext();
        }

        public List<BookListViewModel> GetAllBooks()
        {
            var books = (from b in _db.Books
                         select new BookListViewModel
                         {
                             Id = b.Id,
                             Title = b.Title,
                             Author = b.Author,
                             Image = b.Image,
                             Price = b.Price,
                             Genre = b.Genre,
                             Rating = b.Rating,
                             ReleaseDate = b.ReleaseDate
                         }).ToList();

            return books;
        }
        
        /*public Book GetBook()
        {
            return Book();
        }
        */

        public void AddBook()
        {
            return;
        }

        public void UpdateBook()
        {
            return;
        }
    }
}