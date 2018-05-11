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
        public void AddToWhishlist(string TheUserId, int TheBookId)
        {
            var checker = (from c in _db.Whishlists
                            where c.BookId == TheBookId && c.UserId == TheUserId
                            select c).SingleOrDefault();
            if(checker == null)
            {
                var WhishlistEntityModel = new Whishlist()
                {
                    BookId = TheBookId,
                    UserId = TheUserId,
                };
                _db.Add(WhishlistEntityModel);
                _db.SaveChanges();
            }
        }
        public List<WishlistViewModel> GetWishlistByUserId(string id)
        {
            var WishlistId = (from w in _db.Whishlists
                                where w.UserId == id
                                select new WishlistViewModel
                                {
                                    Id = w.Id,
                                    BookId = w.BookId
                                })
                                .ToList();
            
            return WishlistId;
        }
        public void DeleteWishlist(int id)
        {
            var wishlist = (from wish in _db.Whishlists
                        where id == wish.Id
                        select wish).SingleOrDefault();
            _db.Remove(wishlist);
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

        public void CreateOrder(CartBoughtViewModel info)
        {
            var cart = (from c in _db.Carts
                        where c.UserId == info.UserId
                        select c).ToList();
            var theOrder = new List<Order>();
            for(int i = 0; i < cart.Count(); i++)
            {
                var thebook = (from b in _db.Books
                                where b.Id == cart[i].BookId
                                select b).FirstOrDefault();
                theOrder.Add(new Order {
                    UserId = info.UserId,
                    Title = thebook.Title,
                    Author = thebook.Author,
                    Image = thebook.Image,
                    Price = thebook.Price,
                    Quantity = cart[i].Quantity,
                    FullName = info.FullName,
                    Address = info.ShippingAddress,
                    Country = info.Country,
                    City = info.City,
                    PostCode = info.PostCode,
                    State = info.State
                });
            }
            
            _db.AddRange(theOrder);
            _db.SaveChanges();
            clearCart(info.UserId);
        }
        public List<OrderHistoryViewModels> GetOrders(string id)
        {
            var orders = ( from order in _db.Orders
                            where order.UserId == id
                            orderby order.Id descending
                            select new OrderHistoryViewModels 
                            {
                                UserID = order.UserId,
                                Title = order.Title,
                                Author = order.Author,
                                Image = order.Image,
                                Price = order.Price,
                                Quantity = order.Quantity
                                }).ToList();
            return orders;
        }

        private void clearCart(string UserId)
        {
            var TheCart = (from c in _db.Carts
                            where c.UserId == UserId
                            select c).ToList();
            _db.RemoveRange(TheCart);
            _db.SaveChanges();
        }
    }
}