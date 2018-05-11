using System;
using System.Collections.Generic;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class ReviewService : IReviewService
    {
        private BookService _bookService;
        private BookRepo _bookRepo;

        public ReviewService()
        {
            _bookRepo = new BookRepo();
            _bookService = new BookService();
        }
        public void ProcessReview(ReviewInputModel review)
        {
            if(review.Rating < 1 || 5 < review.Rating) 
            {
                throw new Exception("Rating is invalid!");
            }
            
        }
        
        public List<ReviewViewModel> GetReviews(string id)
        {
            var reviews = _bookRepo.GetReviewsByUserID(id);

            return reviews;
        }

        public void DeleteReview(int reviewId)
        {
            var review = _bookRepo.GetSingleReview(reviewId);
            _bookRepo.DeleteReview(reviewId);
            _bookService.UpdateBookRating(review.BookId);
        }

    }
}