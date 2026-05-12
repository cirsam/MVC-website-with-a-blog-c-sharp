using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myblogs.Data;
using myblogs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace myblogs.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> UserManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;

        public AccountController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager, Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager)
        {
            _context = context;
            UserManager = signInManager;
            _userManager = userManager;
        }




        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Credentials");

            }
            return View(user);
        }



        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action("EmailConfirm", "Account",
                        new { userId = user.Id, code = code }, protocol: Request.Scheme);


                    // await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid");

            }
            return View(model);
        }



      
        [HttpGet]
        public async Task<IActionResult> EmailConfirm(string userId, string code)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user != null)
            {
                await _userManager.ConfirmEmailAsync(user, code);
                ViewBag.user = user.Email;
            }

            return View(); 
        }

    }
}


