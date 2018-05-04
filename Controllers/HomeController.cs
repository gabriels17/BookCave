using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Data.EntityModels;
using BookCave.Services;

namespace BookCave.Controllers
{
    public class HomeController : Controller
    {
        private BookService _bookService;

        public HomeController()
        {
            _bookService = new BookService();
        }

        public IActionResult Index()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }

        /*public IActionResult AddToCart(Book book)
        {
            return View("Index");
        }*/

        public IActionResult Browse(string search)
        {
                var result = _bookService.Search("harry");
                return View(result);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
