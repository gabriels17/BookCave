using System.Collections.Generic;
using System.Data;
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

        public BookDetailsViewModel GetBookById(int id)
        {
            var book = (from b in _db.Books
                         where b.Id == id
                         select new BookDetailsViewModel
                         {
                             Id = b.Id,
                             Title = b.Title,
                             Author = b.Author,
                             Image = b.Image,
                             Price = b.Price,
                             Rating = b.Rating,
                             Genre = b.Genre,
                             ReleaseDate = b.ReleaseDate,
                             Description = b.Description
                         }).SingleOrDefault();
            return(book);
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

        public void AddBook(BookInputModel newBook)
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

        public void DeleteBook(int id)
        {
            var book = (from b in _db.Books
                        where id == b.Id
                        select b).SingleOrDefault();
            _db.Remove(book);
            _db.SaveChanges();
        }

        public void UpdateBook(BookInputModel updatedBook)
        {
            var book = (from b in _db.Books
                        where b.Id == updatedBook.Id
                        select b).SingleOrDefault();
                        
            book.Title = updatedBook.Title;
            book.Author = updatedBook.Author;
            book.Image = updatedBook.Image;
            book.Genre = updatedBook.Genre;
            book.Rating = updatedBook.Rating;
            book.Price = updatedBook.Price;
            book.ReleaseDate = updatedBook.ReleaseDate;
            book.Description = updatedBook.Description;

            _db.SaveChanges();
        }

        public List<ReviewViewModel> GetReviews(int id)
        {
            var reviews = (from r in _db.Reviews
                        where r.BookId == id
                        select new ReviewViewModel
                        {
                            Id = r.Id,
                            Rating = r.Rating,
                            Comment = r.Comment,
                            UserId = r.UserId,
                            BookId = r.BookId
                        }).ToList();
            return reviews;
        }
        public List<ReviewViewModel> GetReviewsByUserID(string id)
        {
            var reviews = (from r in _db.Reviews
                        where r.UserId == id
                        select new ReviewViewModel
                        {
                            Id = r.Id,
                            Rating = r.Rating,
                            Comment = r.Comment,
                            UserId = r.UserId,
                            BookId = r.BookId
                        }).ToList();
                        
            foreach(var review in reviews)
            {
                var book = GetBookById(review.BookId);
                review.BookName = book.Title;
            }

            return reviews;
        }

        public void AddReview(ReviewInputModel newreview)
        {
            var ReviewEntityModel = new Review()
            {
                Comment = newreview.Comment,
                UserId = newreview.UserId,
                Rating = newreview.Rating,
                BookId = newreview.BookId
            };
            _db.Add(ReviewEntityModel);
            _db.SaveChanges();
        }
    }
}