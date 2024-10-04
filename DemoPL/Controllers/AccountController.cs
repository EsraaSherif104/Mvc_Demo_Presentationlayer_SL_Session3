using Demo.DAL.Models;
using DemoPL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace DemoPL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountController(UserManager<ApplicationUser> userManager)
        {
			_userManager = userManager;
		}
        //register
        //basurl/account/register
        public IActionResult Register()
        { return View(); 
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
                    Fname=model.FName,
                    Lname=model.LName,
                    IsAgree=model.IsAgree,
                };
               var result=await _userManager.CreateAsync(User, model.Password);
                if (result.Succeeded)
                
                    return RedirectToAction("Login");
                
                else
                  foreach(var error in result.Errors)
                      ModelState.AddModelError(string.Empty, error.Description);
                    
              
            }
            return View(model);
        }

        //login
        //sign out
        //forget password
        //reset password

    }
}
