using AutoMapper;
using Demo.BLL.Interface;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoPL.Controllers
{
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
        public IActionResult Index()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid) //server side 
            {
                _unitOfWork.DepartmentRepository.Add(department);
                int result=_unitOfWork.Complete();

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


        public IActionResult Details(int? id, string ViewName= "Details")
        {
            if(id == null)
                return BadRequest();//status code 400 client error
            
            var department =  _unitOfWork.DepartmentRepository.GetById(id.Value);
            if(department == null)
                return NotFound();
            return View(ViewName,department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id == null)
            //    return BadRequest();//status code 400 client error

            //var department =  _unitOfWork.DepartmentRepository.GetById(id.Value);
            //if (department == null)
            //    return NotFound();
            //return View(department);
            return Details(id,"Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department,[FromRoute] int id)
        {
            if(id!=department.Id)
                return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                   
                    _unitOfWork.DepartmentRepository.Update(department);
                    _unitOfWork.Complete();
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
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                     _unitOfWork.DepartmentRepository.Delete(department);
                    _unitOfWork.Complete ();
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
