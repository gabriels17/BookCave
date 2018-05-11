using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BookCave.Models;
using Microsoft.AspNetCore.Identity;
using BookCave.Models.ViewModels;
using BookCave.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BookCave.Models.InputModels;

namespace BookCave.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private CartService _cartService;
        private ReviewService _reviewService;
        private BookService _bookService;
        private readonly IAccountService _accountService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IAccountService accountService, RoleManager<IdentityRole> roleManager)
        {
            _cartService = new CartService();
            _reviewService = new ReviewService();
            _bookService = new BookService();
            _signInManager = signInManager;
            _userManager = userManager;
            _accountService = accountService;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterInputModel registerModel)
        {
            if(!ModelState.IsValid) 
            {
                ViewData["ErrorMessage"] = "Error";

                return View();
            }

            _accountService.ProcessRegister(registerModel); //Error handling

            /*IdentityResult roleResult;    //This is commented out because it´s used to create a role which we only needed once to create Admin                                    
            var roleExist = await _roleManager.RoleExistsAsync("Admin");
            if (!roleExist)
            {
                //create the roles and seed them to the database: Question 1
                roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
            }*/

            var user = new ApplicationUser
            {
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                UserName = registerModel.Email, 
                Email = registerModel.Email
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            if(result.Succeeded)
            {
                //await _userManager.AddToRoleAsync(user, "Admin"); //This is commented out because it is used to make the new user admin right away
                await _userManager.AddClaimAsync(user, new Claim("Name", $"{registerModel.FirstName} {registerModel.LastName}"));
                await _signInManager.SignInAsync(user, false);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["ErrorMessage"] = "The information you entered was not valid, please try again";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel loginModel)
        {
            if(!ModelState.IsValid)
            {
                ViewData["ErrorMessages"] = "Error";

                return View();
            }

            _accountService.ProcessLogin(loginModel); //Error handling

            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["ErrorMessage"] = "Email address or password is incorrect";

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> AddToCart(int ID)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            _cartService.AddToCart(userId, ID);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> AddToWishlist(int ID)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            _cartService.AddToWishlist(userId, ID);

            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            // Get User Data
            var user = await _userManager.GetUserAsync(User);
            var reviews = _reviewService.GetReviews(user.Id);
            var orderhistory = _cartService.GetOrderHistory(user.Id);
            var wishlistId = _cartService.GetWishlistId(user.Id);
            var wishlist = _bookService.GetWishlist(wishlistId);

            var profile = new ProfileViewModel 
            {
                Id = user.Id,
                FirstName = user.FirstName, 
                LastName = user.LastName, 
                FavoriteBook = user.FavoriteBook,
                Email = user.Email,
                Image = user.Image,
                FullName = user.FullName,
                ShippingAddress = user.ShippingAddress,
                City = user.City,
                State = user.State,
                Postcode = user.Postcode,
                Country = user.Country,
                Reviews = reviews,
                OrderHistory = orderhistory,
                Wishlist = wishlist
            };

            if (string.IsNullOrEmpty(profile.Image)) //for default profile image when we need one
            {
                profile.Image = "https://cdn.iconscout.com/public/images/icon/free/png-512/avatar-user-hacker-3830b32ad9e0802c-512x512.png";
            }

            return View(profile);
        }
        [Authorize]
        public IActionResult DeleteWishlist(int id)
        {
            _cartService.DeleteWishlist(id);

            return RedirectToAction("MyProfile");
        }

        [Authorize]
        public async Task<IActionResult> EditProfile()
        {
            // Get User Data
            var user = await _userManager.GetUserAsync(User);
            var profile = new ProfileInputModel 
            {
                Id = user.Id,
                FirstName = user.FirstName, 
                LastName = user.LastName, 
                Email = user.Email,
                FavoriteBook = user.FavoriteBook,
                Image = user.Image,
                FullName = user.FullName,
                ShippingAddress = user.ShippingAddress,
                City = user.City,
                State = user.State,
                Postcode = user.Postcode,
                Country = user.Country
            };

            return View(profile);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProfile(ProfileInputModel model)
        {
            if(!ModelState.IsValid)
            {
                ViewData["ErrorMessages"] = "Error";

                return View();
            }
            _accountService.ProcessProfile(model); //Error handling

            var user = await _userManager.GetUserAsync(User);
            
            //Update Properties
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.FavoriteBook = model.FavoriteBook;
            user.Image = model.Image;
            user.FullName = model.FullName;
            user.ShippingAddress = model.ShippingAddress;
            user.City = model.City;
            user.State = model.State;
            user.Postcode = model.Postcode;
            user.Country = model.Country;

            var result = await _userManager.UpdateAsync(user);

            if(result.Succeeded)
            {
                return RedirectToAction("MyProfile", "Account");
            }
            else
            {
                ViewData["ErrorMessage"] = "Failed to save changes, please try again";

                return View(model);
            }
        }

        public IActionResult DeleteReview(int id)
        {
            _reviewService.DeleteReview(id);
            
            return RedirectToAction("MyProfile");
        }
    }
}
