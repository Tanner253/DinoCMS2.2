using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DinoCMS.Models;
using DinoCMS.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DinoCMS.Controllers
{
    public class AccountController : Controller
    {
        private SignInManager<ApplicationUser> _signinManager;
        private UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinmanager)
        {
            _signinManager = signinmanager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                //TODO
                //CHECK TO SEE IF EMAIL / USERNAME ALREADY EXISTS BEFORE CREATING USER
                ApplicationUser user = new ApplicationUser
                {
                    Email = rvm.Email,
                    UserName = rvm.Email,
                    FirstName = rvm.FirstName,
                    LastName = rvm.LastName,
                    Birthday = rvm.Birthday,
                    PrStaff = "false"

                };
                var result = await _userManager.CreateAsync(user, rvm.Password);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "I'm sorry, something went wrong. Please try again.");
                }
                if (result.Succeeded)
                {
                    Claim nameClaim = new Claim("FullName", $"{user.FirstName} { user.LastName} ");

                    Claim emailClaim = new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email);

                    Claim dateOfBirthClaim = new Claim(ClaimTypes.DateOfBirth, new DateTime(user.Birthday.Year, user.Birthday.Month, user.Birthday.Day).ToString("u"), ClaimValueTypes.DateTime);

                    Claim PRSTAFFClaim = new Claim("PrStaff", user.PrStaff);

                    List<Claim> claims = new List<Claim> { nameClaim, emailClaim, dateOfBirthClaim, PRSTAFFClaim};
                    await _userManager.AddClaimsAsync(user, claims);


                    if (rvm.Email.ToLower() == "percivaltanner@gmail.com" ){
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Admin);
                    }else
                    {
                        await _userManager.AddToRoleAsync(user, ApplicationRoles.Member);

                    }
                    return RedirectToAction("Index", "Home");
                }

            }
            return View(rvm);
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel lvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signinManager.PasswordSignInAsync(lvm.Email, lvm.Password, false, false);
                if (result.Succeeded)
                {
                    var signedIn = await _userManager.FindByEmailAsync(lvm.Email);
                    if(await _userManager.IsInRoleAsync(signedIn, ApplicationRoles.Admin))
                    {
                       // return LocalRedirect("~/Admin/Admin");
                       return RedirectToAction("Index", "Dinosaurs");
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(lvm);
        }

        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}