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

        /// <summary>
        /// bring in dependencies
        /// </summary>
        /// <param name="userManager">Access to user</param>
        /// <param name="signinmanager">Allows user information to be used to register and login against indentity</param>
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinmanager)
        {
            _signinManager = signinmanager;
            _userManager = userManager;
        }
        /// <summary>
        /// serves the register view 
        /// </summary>
        /// <returns>serves the view waiting for input data</returns>
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        /// <summary>
        /// takes in data from rvm and if valid assigns data to the user in usermanager
        /// </summary>
        /// <param name="rvm">Register view model</param>
        /// <returns>view</returns>
        [HttpPost]
        public async Task<IActionResult>Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                
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
                    ModelState.AddModelError(string.Empty, "Something went wrong. Please make sure your password includes a capital letter, symbol, and number. (Password1$)");
                }
                if (result.Succeeded)
                {
                    Claim nameClaim = new Claim("FullName", $"{user.FirstName} { user.LastName} ");

                    Claim emailClaim = new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email);

                    Claim dateOfBirthClaim = new Claim(ClaimTypes.DateOfBirth, new DateTime(user.Birthday.Year, user.Birthday.Month, user.Birthday.Day).ToString("u"), ClaimValueTypes.DateTime);

                    

                    List<Claim> claims = new List<Claim> { nameClaim, emailClaim, dateOfBirthClaim};
                    await _userManager.AddClaimsAsync(user, claims);


                    if (rvm.Email.ToLower() == "percivaltanner@gmail.com" || rvm.Email.ToLower() == "jesseshady@gmail.com" || rvm.Email.ToLower() == "jerryleo123455@gmail.com" || rvm.Email.ToLower() == "aandnranch@gmail.com" || rvm.Email.ToLower() == "dvhuus@gmail.com" || rvm.Email.ToLower() == "rayfinkled@yahoo.com")
                    {
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

        /// <summary>
        /// Serves the login page for user input
        /// </summary>
        /// <returns> login page</returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// accepts login information then validates and signs user in if valid.
        /// </summary>
        /// <param name="lvm">Login view model</param>
        /// <returns>redirect to home</returns>
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
                      // return RedirectToAction("List", "Admin");
                       return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(lvm);
        }
        /// <summary>
        /// signs out the user and redirects
        /// </summary>
        /// <returns>Redirect to home</returns>
        public async Task<IActionResult> Logout()
        {
            await _signinManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}