using System.Collections.Generic;
using BookCave.Models.ViewModels;

namespace BookCave.Repositories
{
    public class BookRepo
    {
        public List<BookListViewModel> GetAllBooks()
        {
            var books = new List<BookListViewModel>
            {
                new BookListViewModel
                {
                    Id = 1,
                    Title = "The Catcher in the Rye",
                    Author = "J.D. Salinger",
                    Image = "https://zeerk.com/mod/uploads/2017/03/book.jpg",
                    Price = 7.99,
                    Rating = 4.5
                },

                new BookListViewModel
                {
                    Id = 2,
                    Title = "Harry Potter and the Philosopher's Stone",
                    Author = "J.K. Rowling",
                    Image = "https://zeerk.com/mod/uploads/2017/03/book.jpg",
                    Price = 7.99,
                    Rating = 4.5
                },

                new BookListViewModel
                {
                    Id = 3,
                    Title = "Discrete Mathematics and Its Applications",
                    Author = "Kenneth Rosen",
                    Image = "https://zeerk.com/mod/uploads/2017/03/book.jpg",
                    Price = 7.99,
                    Rating = 4.5
                },

                new BookListViewModel
                {
                    Id = 4,
                    Title = "Harry Potter and the Chamber of Secrets",
                    Author = "J.K. Rowling",
                    Image = "https://zeerk.com/mod/uploads/2017/03/book.jpg",
                    Price = 7.99,
                    Rating = 4.5
                }
            };

            return books;
        }
        









        /*
        public Book GetBook()
        {
            return Book();
        }
        */

        public void AddBook()
        {
            return;
        }

        public void UpdateBook()
        {
            return;
        }
    }
}