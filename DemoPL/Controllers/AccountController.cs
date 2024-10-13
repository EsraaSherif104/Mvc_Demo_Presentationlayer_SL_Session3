using Demo.DAL.Models;
using DemoPL.Helpers;
using DemoPL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace DemoPL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			this._signInManager = signInManager;
		}
        #region Register
        //register
        //basurl/account/register
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    Fname = model.FName,
                    Lname = model.LName,
                    IsAgree = model.IsAgree,
                };
                var result = await _userManager.CreateAsync(User, model.Password);
                if (result.Succeeded)

                    return RedirectToAction(nameof(Login));

                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);


            }
            return View(model);
        }

        //login
        //sign out
        //forget password
        //reset password 
        #endregion
        //Login
        #region Login
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    //login
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        //login
                        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                        if (result.Succeeded)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect password");
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email is not valid");
                }
            }
            return View(model);
        }

        #endregion
        #region sign Out
        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        #endregion
        //forget password
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User =await _userManager.FindByEmailAsync(model.Email);
               if(User is not null)
                {
                    var token=await _userManager.GeneratePasswordResetTokenAsync(User);
                    //vaild for only one time for this user
					//https://localhost:44328/Account/Resetpassword?email=esraasherif9992@gmail.com?Token=hgfjbgdh
					var ResetPasswordLink = Url.Action("ResetPassword", "Account", new { email = User.Email ,Token= token }, Request.Scheme);
					//send email
					var email = new Email()
                    {
                        Subject="Reset Password" ,
                        To=model.Email,
                        Body= "ResetPasswordLink"

					};
                    EmailSetting.SendEmail(email);
                    return RedirectToAction(nameof(CheckYourInbex));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "email is not exists");
                }
            }
           
             return View("ForgetPassword", model);
            

        }

        public IActionResult CheckYourInbex()
        {
            return View();
        }
		//reset
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                string email = TempData["email"] as string;
                string token = TempData["token"] as string;
                var user=await _userManager.FindByEmailAsync(email);
             var result=  await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(Login));
                }
                else
              
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty,error.Description);
                    
                                 
            }
            return View(model);
        }
       





	}
}
