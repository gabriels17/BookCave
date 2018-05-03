using BookCave.Models.ViewModels;
using System.Linq;
using BookCave.Repositories;

namespace BookCave.Repositories
{
    public class CartRepo
    {
        public void AddToCart(int id)
        {
            var cart = new CartViewModel();
            cart.BookId = id;
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