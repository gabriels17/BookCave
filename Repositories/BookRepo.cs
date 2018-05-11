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

        public List<BookListViewModel> GetAllBooks() //The browse page
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

        public BookDetailsViewModel GetBookById(int id) //The book details page
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
        public WishlistViewModel GetWishlistById(int id) //THw wishlist section of the profile page
        {
            var book = (from b in _db.Books
                        where b.Id == id
                        select new WishlistViewModel
                        {
                            BookId = b.Id,
                            Title = b.Title,
                            Author = b.Author,
                            Image = b.Image,
                            Price = b.Price
                        }).SingleOrDefault();
            return(book);
        }

        public List<BookListViewModel> GetTopRated() //The lower half of the home page
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

        public List<BookListViewModel> GetNewReleases() //The upper half of the home page
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

        public void AddBook(BookInputModel newBook) //Adds book to the database
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
            
            _db.Add(BookEntityModel);
            _db.SaveChanges();
        }

        public void DeleteBook(int id) //Delete book from database
        {
            var reviews = (from r in _db.Reviews
                          where r.BookId == id
                          select r).ToList();

             _db.RemoveRange(reviews); //If the book has any reviews, they need to be deleted too

            var wishlists = (from w in _db.Whishlists
                            where w.BookId == id
                            select w).ToList();

            _db.RemoveRange(wishlists); //If the book is on some profile's wishlist, it needs to be removed from those lists

            var carts = (from c in _db.Carts
                        where c.BookId == id
                        select c).ToList();
                        
            _db.RemoveRange(carts); //If the book is in someone's cart, it needs to be removed from the cart

            var book = (from b in _db.Books
                        where id == b.Id
                        select b).SingleOrDefault();

            _db.Remove(book); //At last the book can be removed

            _db.SaveChanges();
            //If any of the parts above are forgotten some functions
            //will throw a NullReferenceException when trying to fetch
            //a book from a cart or a wishlist or when trying to access
            //reviews on a book which no longer exists
        }
        public void DeleteReview(int id) //Deletes a single review
        {
            var review = (from r in _db.Reviews
                        where id == r.Id
                        select r).SingleOrDefault();
            _db.Remove(review);
            _db.SaveChanges();
        }

        public void UpdateBook(BookInputModel updatedBook) //Upates the book in the database
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

        public void UpdateBookRating(int bookid, double newrating) //Updates the book's rating
        {
            var book = (from b in _db.Books
                        where b.Id == bookid
                        select b).SingleOrDefault();

            book.Rating = newrating;

            _db.SaveChanges();
            //This function is used several times in other/functions
        }

        public ReviewViewModel GetSingleReview(int reviewId) //Fetches a single review from the database
        {
            var review = (from r in _db.Reviews
                          where reviewId == r.Id
                          select new ReviewViewModel
                          {
                              Id = r.Id,
                              Rating = r.Rating,
                              Comment = r.Comment,
                              UserId = r.UserId,
                              BookId = r.BookId
                          }).SingleOrDefault();
            return review;
            //Used as a helper function
            //Each review has a bookId which is used
            //in the UpdateBookRating() function
        }

        public List<ReviewViewModel> GetReviews(int id) //Fetches all reviews on a certain book from the database, the book details page
        {
            var reviews = (from r in _db.Reviews
                        where r.BookId == id
                        orderby r.Id descending
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
        public List<ReviewViewModel> GetReviewsByUserID(string id) //Fethes the reviews by user to show on a user's profile page
        {
            var reviews = (from r in _db.Reviews
                        where r.UserId == id
                        orderby r.Id descending
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
                //Each review needs a book name
                // to display on the profile page
            }

            return reviews;
        }

        public void AddReview(ReviewInputModel newreview) //Adds a review to a certain book's details page
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

        public int GetHighestBookId() //A helper function so the random book function has a correct range of books to choose from
        {
            var allBooks = GetAllBooks();
            var book = (from a in allBooks
                      select a).LastOrDefault();
            return book.Id;
        }
    }
}