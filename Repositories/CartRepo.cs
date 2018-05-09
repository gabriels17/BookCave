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
            var CartEntityModel = new Cart()
            {
                BookId = TheBookId,
                UserId = TheUserId
            };
            _db.Add(CartEntityModel);
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
                         }).ToList());
            return cart;
        }
        public void AddOrder()
        {
            return;
        }

        public void AddCart()
        {
            return;
        }
    }
}