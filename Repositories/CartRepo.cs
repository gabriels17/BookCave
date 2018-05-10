using BookCave.Data.EntityModels;
using System.Linq;
using BookCave.Repositories;
using System.Collections.Generic;
using BookCave.Data;
using BookCave.Models.ViewModels;

namespace BookCave.Repositories
{    
    public class CartRepo
    {
        private BookRepo _bookRepo;
        private DataContext _db;

        public CartRepo()
        {
            _bookRepo = new BookRepo();
            _db = new DataContext();
        }
        public void AddToCart(string TheUserId, int TheBookId)
        {
            var checker = (from c in _db.Carts
                            where c.BookId == TheBookId && c.UserId == TheUserId
                            select c).SingleOrDefault();
            if(checker == null)
            {
                var CartEntityModel = new Cart()
                {
                    BookId = TheBookId,
                    UserId = TheUserId,
                    Quantity = 1
                };
                _db.Add(CartEntityModel);
            }
            else
            {
                checker.Quantity = checker.Quantity + 1;
            }
            _db.SaveChanges();
        }

        public  List<CartViewModel> GetCart (string TheUserId)
        {
            var cart = ((from c in _db.Carts
                        join b in _db.Books on c.BookId equals b.Id
                        where c.UserId == TheUserId
                         select new CartViewModel
                         {
                             Id = b.Id,
                             Title = b.Title,
                             Author = b.Author,
                             Image = b.Image,
                             Price = b.Price,
                             Genre = b.Genre,
                             Rating = b.Rating,
                             Quantity = c.Quantity
                         }).ToList());
            return cart;
        }

        public void UpdateCart(int bookId, int quantity, string userId)
        {
            var checker = (from c in _db.Carts
                            where c.BookId == bookId && c.UserId == userId
                            select c).SingleOrDefault();

                checker.Quantity = quantity;
                _db.SaveChanges();
        }
        
        public void RemoveFromCart(int bookId, string userId)
        {
            var checker = (from c in _db.Carts
                            where c.BookId == bookId && c.UserId == userId
                            select c).SingleOrDefault();
                _db.Remove(checker);
                _db.SaveChanges();
        }
    }
}