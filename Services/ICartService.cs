using BookCave.Models.InputModels;

namespace BookCave.Services
{
    public interface ICartService
    {
        void ProcessCart(CheckoutInputModel cart);   
    }
}