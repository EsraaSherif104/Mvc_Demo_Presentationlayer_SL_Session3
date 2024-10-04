using Demo.DAL.Models;
using DemoPL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoPL.Controllers
{
	public class UsersController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UsersController(UserManager<ApplicationUser>UserManager)
        {
			_userManager = UserManager;
		}
        public async Task<IActionResult> Index(string searchValue)
		{
			if(string.IsNullOrEmpty(searchValue))
			{
				var Users =await _userManager.Users.Select
					(x => new UserViewModel()
					{
						id = x.Id,
						Fname = x.Fname,
						Lname = x.Lname,
						Email = x.Email,
						PhoneNumber = x.PhoneNumber,
						Roles = _userManager.GetRolesAsync(x).Result

					}).ToListAsync();
				return View(Users);

			}
			else
			{
				var User=await _userManager.FindByEmailAsync(searchValue);
				var MappedUser = new UserViewModel
				{
					id = User.Id,
					Fname = User.Fname,
					Lname = User.Lname,
					Email = User.Email,
					PhoneNumber = User.PhoneNumber,
					Roles = _userManager.GetRolesAsync(User).Result
				};
				return View(new List<UserViewModel> { MappedUser});
			}
		}
	}
}
