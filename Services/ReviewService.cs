using System;
using BookCave.Models.InputModels;

namespace BookCave.Services
{
    public class ReviewService : IReviewService
    {
        public void ProcessReview(ReviewInputModel review)
        {
            if(review.Rating <= 0 || 5 < review.Rating) 
            {
                throw new Exception("Rating is invalid!");
            }
            
        }
    }
}