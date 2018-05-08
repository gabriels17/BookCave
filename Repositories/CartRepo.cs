using BookCave.Data.EntityModels;
using System.Linq;
using BookCave.Repositories;
using BookCave.Data;

namespace BookCave.Repositories
{    
    public class CartRepo
    {
        private DataContext _db;

        public CartRepo()
        {
            _db = new DataContext();
        }

        public void AddToCart(string TheUserId, int TheBookId)
        {
            var CartEntityModel = new Cart()
            {
                BookId = TheBookId,
                UserId = TheUserId
            };
            _db.AddRange(CartEntityModel);
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