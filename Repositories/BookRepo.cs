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
                    Name = "The Catcher in the Rye",
                    Image = "https://zeerk.com/mod/uploads/2017/03/book.jpg"
                },

                new BookListViewModel
                {
                    Id = 2,
                    Name = "Harry Potter and the Philosopher's Stone",
                    Image = "https://zeerk.com/mod/uploads/2017/03/book.jpg"
                },

                new BookListViewModel
                {
                    Id = 3,
                    Name = "Discrete Mathematics and Its Applications",
                    Image = "https://zeerk.com/mod/uploads/2017/03/book.jpg"
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