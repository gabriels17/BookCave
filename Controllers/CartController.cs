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

namespace BookCave.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private CartService _cartService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CartController(UserManager<ApplicationUser> userManager)
        {
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

        public async Task<IActionResult> CheckoutInformation()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            var info = new CheckoutViewModel {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                ShippingAddress = user.ShippingAddress,
                City = user.City,
                State = user.State,
                Postcode = user.Postcode,
                Country = user.Country
            };

            return View(info);
        }

    }
}
