using BookCave.Data.EntityModels;
using System.Linq;
using BookCave.Repositories;
using System.Collections.Generic;
using BookCave.Data;

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

        public void AddToCart(int id, string userId)
        {
            var cart = new Cart();
            cart.BookId = id;
            cart.UserId = userId;
            var books = (from b in _bookRepo.GetAllBooks()
                         where cart.BookId == id
                         select b).ToList();
            _db.Add(cart);
            _db.SaveChanges();
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










        /*
        public List<BookViewModel> GetBooks()
        {
            return List<BookViewModel();
        }
        */
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