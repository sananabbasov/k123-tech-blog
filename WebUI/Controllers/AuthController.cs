using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebUI.DTOs;
using WebUI.Helpers;
using WebUI.Models;
using WebUI.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _singInManager;
        private readonly EmailHelper _emailHelper;

        public AuthController(UserManager<User> userManager, EmailHelper emailHelper, SignInManager<User> singInManager)
        {
            _userManager = userManager;
            _emailHelper = emailHelper;
            _singInManager = singInManager;
        }

        // GET: /<controller>/
        public IActionResult Login(string? id)
        {
            if (id != null)
            {
                ViewBag.Confirmation = "Emailinizə təsdiq mesajı göndərildi.";
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO userLogin)
        {
            string fromDetail = HttpContext.Request.Query["returnUrl"].ToString();


            var findUser = await _userManager.FindByEmailAsync(userLogin.Email);
            if (findUser == null)
            {
                return View();
            }


            Microsoft.AspNetCore.Identity.SignInResult result = await _singInManager.PasswordSignInAsync(findUser,userLogin.Password,false,false);

            if (result.Succeeded)
            {
                if (fromDetail.Length != 0)
                {
                    var returnId = fromDetail.Split("/").ToArray();
                    return Redirect($"/article/detail/{returnId[2]}/{returnId[3]}");
                }
                return RedirectToAction("Index", "Home");
            }

            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO userInput)
        {
            var checkUser = await _userManager.FindByEmailAsync(userInput.Email);
            if (checkUser != null)
            {
                return View();
            }
            User user = new()
            {
                FirstName = userInput.FirstName,
                LastName = userInput.LastName,
                PhotoUrl = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png",
                About = " ",
                Email = userInput.Email,
                UserName = userInput.Email,
                ConfirmationToken = Guid.NewGuid().ToString(),
                ConfirmationTokenEndDate = DateTime.Now.AddMinutes(1),
            };

            var result = await _userManager.CreateAsync(user, userInput.Password);



            if (result.Succeeded)
            {
                _emailHelper.SendConfirmationEmail(user.Email, user.FirstName, user.LastName, user.ConfirmationToken);

                return RedirectToAction("Login", new { Id = "Confirmation" });
            }

            return View(userInput);
        }


        public async Task<IActionResult> Confirmation(string id, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("Login");
        }


        public async Task<IActionResult> Logout()
        {
            await _singInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}




