using System;
using System.Collections.Generic;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;
using BookCave.Repositories;

namespace BookCave.Services
{
    public class CartService : ICartService
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

        public void UpdateCart(int bookId, int quantity, string userId)
        {
            _cartRepo.UpdateCart(bookId, quantity, userId);
        }

        public void RemoveFromCart(int bookId, string userId)
        {
             _cartRepo.RemoveFromCart(bookId, userId);
        }

        public void CreateOrder(CartBoughtViewModel info)
        {
            _cartRepo.CreateOrder(info);
        }
        public List<OrderHistoryViewModels> GetOrderHistory(string id)
        {
            var orderhistory = _cartRepo.GetOrders(id);
            return orderhistory;
        }
        public List<WishlistViewModel> GetWishlistId(string id)
        {
            var wishlist = _cartRepo.GetWishlistByUserId(id);
            return wishlist;
        }
        public void DeleteWishlist(int id)
        {
            _cartRepo.DeleteWishlist(id);
        }

        public void ProcessCart(CheckoutInputModel cart)
        {
            if(string.IsNullOrEmpty(cart.Email))
            {
                throw new Exception("Email is missing");
            }

            if(string.IsNullOrEmpty(cart.FullName))
            {
                throw new Exception("Full name is missing");
            }

            if(string.IsNullOrEmpty(cart.ShippingAddress))
            {
                throw new Exception("Address is missing");
            }

            if(string.IsNullOrEmpty(cart.City))
            {
                throw new Exception("City is missing");
            }

            if(string.IsNullOrEmpty(cart.PostCode))
            {
                throw new Exception("Postcode is missing");
            }

            if(string.IsNullOrEmpty(cart.Country))
            {
                throw new Exception("Country is missing");
            }

            if(string.IsNullOrEmpty(cart.CardNumber))
            {
                throw new Exception("Card number is missing");
            }

            if(string.IsNullOrEmpty(cart.ExpMonth))
            {
                throw new Exception("Expire month is missing");
            }

            if(string.IsNullOrEmpty(cart.ExpYear))
            {
                throw new Exception("Expire year is missing");
            }

            if(string.IsNullOrEmpty(cart.SecurityCode))
            {
                throw new Exception("Security code is missing");
            }
        }
        public void AddToWhishlist(string UserId, int BookId)
        {
            _cartRepo.AddToWhishlist(UserId, BookId);
        }
    }
}