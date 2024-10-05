using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using DemoPL.ViewModels;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Authorization; // For ToListAsync()



namespace DemoPL.Controllers
{
    [Authorize]

    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole>RoleManager,IMapper mapper)
        {
            _roleManager = RoleManager;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchValue)
        {
            if (string.IsNullOrEmpty(searchValue))
            {

                var roles = await _roleManager.Roles.ToListAsync();
                var MappedRole = _mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(roles);
                return View(MappedRole);

            }
            else
            {
              var Role=await _roleManager.FindByNameAsync(searchValue);
                var mappedRole = _mapper.Map<IdentityRole, RoleViewModel>(Role);
                return View(new List<RoleViewModel>(){ mappedRole});
            }
           
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
             if (ModelState.IsValid)
            {
                var MappedRole=_mapper.Map<RoleViewModel,IdentityRole> (model);
               await _roleManager.CreateAsync(MappedRole);
                return RedirectToAction("Index");
            }
             return View(model);
        }
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound();
            var MappedRole = _mapper.Map<IdentityRole, RoleViewModel>(role);

            return View(ViewName, MappedRole);
        }


        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RoleViewModel model, [FromRoute] string id)
        {
            if (id != model.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {

                    // var MappedUser = _Mapper.Map<UserViewModel, ApplicationUser>(model);
                    var Role = await _roleManager.FindByIdAsync(id);
                    Role.Name = model.RoleName;
                 

                    await _roleManager.UpdateAsync(Role);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

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
                var Role = await _roleManager.FindByIdAsync(id);
                await _roleManager.DeleteAsync(Role);
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
