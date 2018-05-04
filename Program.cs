using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookCave.Data;
using BookCave.Data.EntityModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BookCave
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            //SeedData();
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public static void SeedData()
        {
            var db = new DataContext();

            if(!db.Books.Any())
            {
                var initialBooks = new List<Book>()
                {
                    new Book
                    {
                        Title = "The Catcher in the Rye",
                        Author = "J.D. Salinger",
                        Image = "https://zeerk.com/mod/uploads/2017/03/book.jpg",
                        Price = 7.99,
                        Rating = 1.5
                    },

                    new Book
                    {
                        Title = "Harry Potter and the Philosopher's Stone",
                        Author = "J.K. Rowling",
                        Image = "https://zeerk.com/mod/uploads/2017/03/book.jpg",
                        Price = 7.99,
                        Rating = 4.5
                    },

                    new Book
                    {
                        Title = "Discrete Mathematics and Its Applications",
                        Author = "Kenneth Rosen",
                        Image = "https://zeerk.com/mod/uploads/2017/03/book.jpg",
                        Price = 7.99,
                        Rating = 4
                    }
                };

                db.AddRange(initialBooks);
                db.SaveChanges();
            }
        }
    }
}
