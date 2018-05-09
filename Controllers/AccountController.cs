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

        private readonly IAccountService _accountService;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IAccountService accountService)
        {
            _cartService = new CartService();
            _signInManager = signInManager;
            _userManager = userManager;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerModel)
        {
            if(!ModelState.IsValid)
            {
                ViewData["ErrorMessage"] = "Error";
                return View();
            }

            _accountService.ProcessRegister(registerModel);



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
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if(!ModelState.IsValid)
            {
                ViewData["ErrorMessages"] = "Error";
                return View();
            }

            _accountService.ProcessLogin(loginModel);

            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);
            if(result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["ErrorMessage"] = "Email or password is incorrect";
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

        public async Task<IActionResult> AddToCart(int ID)
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = user.Id;
            _cartService.AddToCart(userId, ID);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            // Get User Data
            var user = await _userManager.GetUserAsync(User);
            var profile = new ProfileViewModel 
            {
                Id = user.Id,
                FirstName = user.FirstName, 
                LastName = user.LastName, 
                FavoriteBook = user.FavoriteBook,
                Email = user.Email,
                Image = user.Image
            };

            return View(profile);
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
                FavoriteBook = user.FavoriteBook,
                Image = user.Image
            };

            return View(profile);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProfile(ProfileInputModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            
            //Update Properties
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.FavoriteBook = model.FavoriteBook;
            user.Image = model.Image;

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

        public void AddReview()
        {
            
        }
    }
}
