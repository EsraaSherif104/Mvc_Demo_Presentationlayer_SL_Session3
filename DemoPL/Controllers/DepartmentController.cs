using AutoMapper;
using Demo.BLL.Interface;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DemoPL.Controllers
{
	[Authorize]

	public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //inheritance DepartmentController is Controller
        //aggregation DepartmentController has DepartmentRepository
        public DepartmentController(IUnitOfWork unitOfWork   , IMapper mapper) 
        {
            this._unitOfWork = unitOfWork;
            //   _unitOfWork.DepartmentRepository=new iDepartmentRepository();
        }
        //basURL/vontroller/index
        public async Task<IActionResult> Index()
        {
            var departments =await _unitOfWork.DepartmentRepository.GetAllAsync();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if(ModelState.IsValid) //server side 
            {
              await  _unitOfWork.DepartmentRepository.AddAsync(department);
                int result=await _unitOfWork.CompleteAsync();

                //3.temp data ->dictionary object 
                //transfer action to action 
                if (result > 0)
                {
                    TempData["message"] = "Department is created";

                }

                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }


        public async Task<IActionResult> Details(int? id, string ViewName= "Details")
        {
            if(id == null)
                return BadRequest();//status code 400 client error
            
            var department = await _unitOfWork.DepartmentRepository.GetByIdAsync(id.Value);
            if(department == null)
                return NotFound();
            return View(ViewName,department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //    return BadRequest();//status code 400 client error

            //var department =  _unitOfWork.DepartmentRepository.GetById(id.Value);
            //if (department == null)
            //    return NotFound();
            //return View(department);
            return await Details(id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department,[FromRoute] int id)
        {
            if(id!=department.Id)
                return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                   
                    _unitOfWork.DepartmentRepository.Update(department);
                   await _unitOfWork.CompleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex) 
                {
                    //1-log excep
                    //2-form
                    ModelState.AddModelError(string.Empty, ex.Message);
                     
                }
                

            }
            return View(department);
           
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                     _unitOfWork.DepartmentRepository.Delete(department);
                   await _unitOfWork.CompleteAsync ();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    //1-log excep
                    //2-form
                    ModelState.AddModelError(string.Empty, ex.Message);

                }


            }
            return View(department);

        }
      



    }
}
