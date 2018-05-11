﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Data.EntityModels;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;
using BookCave.Models.InputModels;
using BookCave.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using BookCave.Models.InputViewModels;

namespace BookCave.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookServiceError;

        private readonly IReviewService _reviewService;
        private readonly UserManager<ApplicationUser> _userManager;

        private BookService _bookService;

        public HomeController(IBookService bookService, UserManager<ApplicationUser> userManager, IReviewService reviewService)
        {

            _userManager = userManager;
            _bookServiceError = bookService;
            _bookService = new BookService();
            _reviewService = reviewService;
        }

        public IActionResult Index()
        {
            var books = _bookService.GetHomePage();

            return View(books);
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        public IActionResult AddToCart(Book book)
        {
            return View("Index");
        }

        public IActionResult Browse(string sortOrder, string search, string genre)
        {
            ViewBag.SortParm = sortOrder;
            var books = _bookService.GetAllBooks();
            

            if(!String.IsNullOrEmpty(search))
            {
                books = _bookService.Search(search,books);
            }

            if(!String.IsNullOrEmpty(genre))
            {
                books =_bookService.Filter(genre,books);
            }

            switch (sortOrder)
            {
                case "Az":
                    books = _bookService.SortByAz(books);
                    break;
                case "Za":
                    books = _bookService.SortByZa(books);
                    break;
                case "Rating":
                    books = _bookService.SortByRating(books);
                    break;
                case "PriceHigh":
                    books = _bookService.SortByPriceHigh(books);
                    break;
                case "PriceLow":
                    books = _bookService.SortByPriceLow(books);
                    break;
                case "DateNew":
                    books = _bookService.SortByReleaseNewest(books);
                    break;
                case "DateOld":
                    books = _bookService.SortByReleaseOldest(books);
                    break;
                default:
                    break;
            }
            
            return View(books);
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddBook(BookInputModel newBook)
        {
            if(!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Error";
                return View();
            }
            _bookServiceError.ProcessBook(newBook);
            _bookService.AddBook(newBook);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            if(id == 0)
            {
                return View("Error");
            }
            var idbook = _bookService.GetBookById(id);
            var reviews = _bookService.GetReviews(id);
            var detail = new DetailsInputViewModel();

            var username = _userManager.Users;
            _bookService.ChangeUserIdToName(reviews, username);

            detail.Book = idbook;
            detail.Reviews = reviews;
            return View(detail);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Details(int BookId, DetailsInputViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Error";
                return RedirectToAction("Details", "Home");
            }
            var review = model.Input;
            review.BookId = BookId;
            _reviewService.ProcessReview(review);
            var user = await _userManager.GetUserAsync(User);
            review.UserId = user.Id;
            _bookService.AddReview(review);
           return RedirectToAction("Details", "Home");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Edit(int id)
        {
            var bookToEdit = _bookService.GetBookById(id);
            ViewData["Name"] = bookToEdit.Title;
            return View(bookToEdit);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Edit(BookInputModel book)
        {
            ViewData["Name"] = book.Title;
            if(!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Error";
                return View("Error");
            }
            _bookServiceError.ProcessBook(book);
            _bookService.UpdateBook(book);
            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            _bookService.DeleteBook(id);
            return RedirectToAction("Index");
        }

        public IActionResult GoToRandomBook()
        {
            var randomBook = _bookService.GoToRandomBook();
            return View("Details", randomBook);
        }
    }
}
