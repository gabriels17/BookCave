using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using BookCave.Models.ViewModels;
using BookCave.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BookCave.Models.InputModels;

namespace BookCave.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private CartService _cartService;
        private readonly ICartService _cartServiceError;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(UserManager<ApplicationUser> userManager, ICartService cartServiceError)
        {
            _cartServiceError = cartServiceError;
            _cartService = new CartService();
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var thecart = _cartService.GetCart(userId);

            return View(thecart);
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int ID)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            _cartService.AddToCart(userId, ID);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart(int bookId, int quantity)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            _cartService.UpdateCart(bookId, quantity, userId);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int bookId)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            _cartService.RemoveFromCart(bookId, userId);
            
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CheckoutInformation(bool error)
        {
            if(error == true)
            {
                ViewData["ErrorMessage"] = "Error";
            }
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var info = new CheckoutInputModel {
                UserId = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                ShippingAddress = user.ShippingAddress,
                City = user.City,
                State = user.State,
                PostCode = user.Postcode,
                Country = user.Country,
            };

            return View(info);
        }

        public IActionResult BuyingCart(CheckoutInputModel info)
        {
            if(!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Error";

                return RedirectToAction("CheckoutInformation", "Cart", new {error = true});
            }
            
            _cartServiceError.ProcessCart(info); 
            var buyingcartinfo = new BuyCartViewModel {
                TheCart = _cartService.GetCart(info.UserId),
                Info = info
            };

            return View(buyingcartinfo);
        }

        [HttpPost]
        public IActionResult CartBought(CartBoughtViewModel info)
        {
            _cartService.CreateOrder(info);

            return RedirectToAction("ThankYou", new { email = info.Email });
        }

        public IActionResult ThankYou(string email)
        {
            ViewData["Email"] = email;
            
            return View();
        }

    }
}
