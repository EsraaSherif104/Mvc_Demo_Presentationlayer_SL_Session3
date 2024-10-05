using AutoMapper;
using Demo.DAL.Models;
using DemoPL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoPL.Controllers
{
    public class UsersController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public readonly IMapper _Mapper;

        public UsersController(UserManager<ApplicationUser>UserManager,
			IMapper mapper)
        {
			_userManager = UserManager;
            _Mapper = mapper;
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
		public async Task<IActionResult> Details(string id,string ViewName= "Details")
		{
			if(id is null)
				return BadRequest();
		       var user=   await _userManager.FindByIdAsync(id);
			if(user is null)
				return NotFound();
			var MappedUser = _Mapper.Map<ApplicationUser, UserViewModel>(user);

			return View(ViewName,MappedUser);
		}


		public async Task< IActionResult> Edit(string id)
		{
			return await Details(id, "Edit");
		}
		[HttpPost]
		public async Task<IActionResult>Edit(UserViewModel model, [FromRoute]string id)
		{
			if (id != model.id)
				return BadRequest();
			if (ModelState.IsValid)
			{
				try
				{

					// var MappedUser = _Mapper.Map<UserViewModel, ApplicationUser>(model);
			   var User=	await	_userManager.FindByIdAsync(id);
					User.PhoneNumber= model.PhoneNumber;
					User.Fname= model.Fname;
					User.Lname= model.Lname;
			
					await _userManager.UpdateAsync(User);
                    return RedirectToAction(nameof(Index));
                }
				catch(Exception ex)
				{
					ModelState.AddModelError(string.Empty,ex.Message);	

				}
			}
			return View(model);
		}
	
	public async Task<IActionResult> Delete(string id)
		{
			return await Details(id, "Delete");
		}
		[HttpPost]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
			try
			{
				var User=await _userManager.FindByIdAsync(id);
			    await	_userManager.DeleteAsync(User);
				return RedirectToAction(nameof(Index));


			}
			catch (Exception ex)
			{
				ModelState.AddModelError(string.Empty, ex.Message);
				return RedirectToAction("Error", "Home");
			}

        }


    }
}
