using System.Collections.Generic;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class CartService
    {
        private CartRepo _cartRepo;

        public CartService()
        {
            _cartRepo = new CartRepo();
        }

        public void AddToCart(string UserId, int BookId)
        {
            _cartRepo.AddToCart(UserId, BookId);
        }

        public List<CartViewModel> GetCart(string userId)
        {   
            var thecart = _cartRepo.GetCart(userId);

            return thecart;
        }
    }
}