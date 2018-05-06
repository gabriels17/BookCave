using BookCave.Models.ViewModels;
using System.Linq;
using BookCave.Repositories;

namespace BookCave.Repositories
{    
    public class CartRepo
    {
        private BookRepo _bookRepo;

        public CartRepo()
        {
            _bookRepo = new BookRepo();
        }

        public void AddToCart(int id)
        {
            var cart = new CartViewModel();
            cart.BookId = id;
            var books = (from b in _bookRepo.GetAllBooks()
                         where cart.BookId == id
                         select b).ToList();
            
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