using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using DemoPL.ViewModels; // For ToListAsync()



namespace DemoPL.Controllers
{
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
    }
}
