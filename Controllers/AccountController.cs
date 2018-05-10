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
        public async Task<IActionResult> Register(RegisterInputModel registerModel)
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
        public async Task<IActionResult> Login(LoginInputModel loginModel)
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

            if (string.IsNullOrEmpty(profile.Image))
            {
                profile.Image = "https://cdn.pixabay.com/photo/2013/07/12/19/15/gangster-154425_960_720.png";
            }

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

            if (string.IsNullOrEmpty(profile.Image))
            {
                profile.Image = "https://cdn.pixabay.com/photo/2013/07/12/19/15/gangster-154425_960_720.png";
            }

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
            _accountService.ProcessProfile(model);
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

        // private async Task createRolesandUsers()
        // {  
        //     if (!await _roleManager.RoleExistsAsync("Admin"))
        //     {
        //         var role = new IdentityRole();
        //         role.Name = "Admin";
        //         await _roleManager.CreateAsync(role);

        //         var user = new ApplicationUser();
        //         user.UserName = "admin";
        //         user.Email = "admin@bookcave.com";
        //         string userPWD = "admin";

        //         IdentityResult newUser = await _userManager.CreateAsync(user, userPWD);

        //         //Add default User to Role Admin
        //         if (newUser.Succeeded)
        //         {
        //             var result1 = await _userManager.AddToRoleAsync(user, "admin");
        //         }
        //     }

        //     // Creating Customer role     
        //     x = await _roleManager.RoleExistsAsync("Customer");
        //     if (!x)
        //     {
        //         var role = new IdentityRole();
        //         role.Name = "Employee";
        //         await _roleManager.CreateAsync(role);
        //     }
        // }
    }
}
