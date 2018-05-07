using System.Collections.Generic;
using System.Linq;
using BookCave.Data;
using BookCave.Data.EntityModels;
using BookCave.Models.InputModels;
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

        public List<BookListViewModel> GetTopRated()
        {
            var books = (from b in _db.Books
                        orderby b.Rating descending
                        select new BookListViewModel
                        {
                            Id = b.Id,
                            Title = b.Title,
                            Author = b.Author,
                            Image = b.Image,
                            Price = b.Price,
                            Rating = b.Rating,
                            ReleaseDate = b.ReleaseDate
                        }).Take(10).ToList();

            return books;
        }

        public List<BookListViewModel> GetNewReleases()
        {
            var books = (from b in _db.Books
                        orderby b.ReleaseDate descending
                        select new BookListViewModel
                        {
                            Id = b.Id,
                            Title = b.Title,
                            Author = b.Author,
                            Image = b.Image,
                            Price = b.Price,
                            Rating = b.Rating,
                            ReleaseDate = b.ReleaseDate
                        }).Take(10).ToList();

            return books;
        }
        
        /*public Book GetBook()
        {
            return Book();
        }
        */

        public void AddToDatabase(BookInputModel newBook)
        {
            var BookEntityModel = new Book()
            {
                Title = newBook.Title,
                Author = newBook.Author,
                Image = newBook.Image,
                Price = newBook.Price,
                Genre = newBook.Genre,
                ReleaseDate = newBook.ReleaseDate,
                Rating = newBook.Rating,
                Description = newBook.Description
            };
            _db.AddRange(BookEntityModel);
            _db.SaveChanges();
        }

        public void UpdateBook()
        {
            return;
        }
    }
}