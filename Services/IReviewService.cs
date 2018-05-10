using BookCave.Models.InputModels;

namespace BookCave.Services
{
    public interface IReviewService
    {
        void ProcessReview(ReviewInputModel book);   
    }
}