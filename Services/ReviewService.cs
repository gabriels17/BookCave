using System;
using System.Collections.Generic;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class ReviewService : IReviewService
    {
        private BookRepo _bookRepo;

        public ReviewService()
        {
            _bookRepo = new BookRepo();
        }
        public void ProcessReview(ReviewInputModel review)
        {
            if(review.Rating <= 0 || review.Rating > 5) 
            {
                throw new Exception("Rating is invalid!");
            }
            
        }
        public List<ReviewViewModel> GetReviews(string id)
        {
            var reviews = _bookRepo.GetReviewsByUserID(id);
            
            return reviews;
        }
    }
}