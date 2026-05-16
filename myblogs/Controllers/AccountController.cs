using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using myblogs.Data;
using myblogs.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace myblogs.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> UserManager;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
        private bool sendMessageStatus;

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
                else
                {

                    var systemUser = await _userManager.FindByEmailAsync(user.Email);
                    if (systemUser != null)
                    {
                        bool isConfirmed = await _userManager.IsEmailConfirmedAsync(systemUser);
                        if (!isConfirmed)
                        {
                            ModelState.AddModelError(string.Empty, "your email is not confirmed we cannot log you in until you confim your email.Check your junk mail or spamfolder");
                        }


                    }


                }
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
                    // await _signInManager.SignInAsync(user, isPersistent: false); Ucomment this line to automaically log the user in when they register.Which is not seured.

                    // they should confirm their account with the callbackUrl link in the email that is geneated below before they should be allowed to login.

                    string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                   
                        var emailConfirmationLink = Url.Action("EmailConfirm", "Account", new { userId = user.Id, code = token}, protocol: HttpContext.Request.Scheme);

                    if (!string.IsNullOrEmpty(emailConfirmationLink))
                    {
                        await SentAccountConfirmationEmail(model.Email, emailConfirmationLink);
                    }

                    return RedirectToAction("SentAccountConfirmationEmail", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                ModelState.AddModelError(string.Empty, "Invalid");

            }
            return View(model);
        }



        public async Task<IActionResult> SentAccountConfirmationEmail(string userEmail, string emailConfirmationLink)
        {
            try
            {
               //to send an email from visual studio localhost server to an external email address, go to your gmail account security settings and create an app password ,give your app a name
               //they will give you a 16 digit password
               //and use your email and app name with the credentials here you should be good to go if you gollow the steps here.
                 
                var senderEmail = new MailAddress("your_email_name@gmail.com", "AppName");
                var receiverEmail = new MailAddress(userEmail);
                //var password = "your-16-char-app-password"; // Use the App Password here
                var password = ""; // Use the App Password here
                var subject = "put your email subject here";
                var body = $"Please click the link <a href=\"{emailConfirmationLink}\">confirm</a> to confirm your account...";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };

                using (var message = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    await smtp.SendMailAsync(message);
                    sendMessageStatus = true;
                    if (sendMessageStatus)
                    {
                        ViewBag.Message = "Confirmation email sent successfully!";

                    }
                    else
                    {
                        ViewBag.Message = "Failed to send confirmation email.Contact Adiminstrator at cirsam...";
                    }
                }

                } catch (Exception ex)
                    {
                        ViewBag.Message = "Error: system failure";
                    }

            return View();
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


