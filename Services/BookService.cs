using System;
using System.Collections.Generic;
using System.Linq;
using BookCave.Models;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services 
{
    public class BookService : IBookService
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
            var book = _bookRepo.GetBookById(id);
            return book; 
        }

        public List<ReviewViewModel> GetReviews(int id)
        {
            var reviews = _bookRepo.GetReviews(id);
            return(reviews);
        }

        public void ChangeUserIdToName(List<ReviewViewModel> reviews, IQueryable<ApplicationUser> username)
        {
            foreach(var r in reviews)
            {
                foreach(var u in username)
                {
                    if(r.UserId == u.Id)
                    {
                        r.UserId = u.FirstName;
                    } 
                }
            }
            // return(reviews);
        }

        public void AddReview(ReviewInputModel review)
        {
            var newRating = FindAverageRating(review);
            _bookRepo.UpdateBookRating(review.BookId, newRating);
            _bookRepo.AddReview(review);
        }

        private double FindAverageRating(ReviewInputModel review)
        {
            var incomingRating = review.Rating;
            var reviews = _bookRepo.GetReviews(review.BookId);

            var sumOfRatings = 0.0;
            foreach(var r in reviews)
            {
                sumOfRatings += r.Rating;
            }

            sumOfRatings += incomingRating;
            var numberOfReviews = reviews.Count + 1;
            var newRating = sumOfRatings / numberOfReviews;

            return newRating;
        }
        public void AddBook(BookInputModel newBook)
        {
            _bookRepo.AddBook(newBook);
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

        public void DeleteBook(int id)
        {
            _bookRepo.DeleteBook(id);
        }

        public void UpdateBook(BookInputModel book)
        {
            _bookRepo.UpdateBook(book);
        }

        public void ProcessBook(BookInputModel book)
        {

            if(string.IsNullOrEmpty(book.Title))
            {
                throw new Exception("Title is missing!");
            }

            if(string.IsNullOrEmpty(book.Author))
            {
                throw new Exception("Author is missing!");
            }

            if(string.IsNullOrEmpty(book.Genre))
            {
                throw new Exception("Genre is missing!");
            }

            if(book.Price <= 0)
            {
                throw new Exception("Prixe is invalid!");
            }

            if(book.ReleaseDate.Year <= 0)
            {
                throw new Exception("Release date year is invalid");
            }

            if(book.ReleaseDate.Month <= 0)
            {
                throw new Exception("Release date month is invalid");
            }

            if(book.ReleaseDate.Day <= 0)
            {
                throw new Exception("Release date day is invalid");
            }

            if(string.IsNullOrEmpty(book.Image))
            {
                throw new Exception("Image is missing");
            }

            if(string.IsNullOrEmpty(book.Description))
            {
                throw new Exception("Description is missing!");
            }
        }
        public int GetHighestBookId()
        {
            return _bookRepo.GetHighestBookId();
        }
    }
}
