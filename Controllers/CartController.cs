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

        public IActionResult Index()
        {
            return View();
        }

        public void AddToCart()
        {
            //_cartService.AddToCart();
        }
    }
}
