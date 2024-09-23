using Demo.BLL.Interface;
using Demo.BLL.Repositories;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoPL.Controllers
{
    public class DepartmentController : Controller
    {
        //inheritance DepartmentController is Controller
        //aggregation DepartmentController has DepartmentRepository
        private readonly IDepartmentRepository _departRepo;
        public DepartmentController(IDepartmentRepository departmentRepository)//ask clr for creating object from class implement interface
        {
            _departRepo = departmentRepository;
          //  _departRepo=new iDepartmentRepository();
        }
        //basURL/vontroller/index
        public IActionResult Index()
        {
            var departments = _departRepo.GetAll();
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
                _departRepo.Add(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }


        public IActionResult Details(int? id, string ViewName= "Details")
        {
            if(id == null)
                return BadRequest();//status code 400 client error
            
            var department = _departRepo.GetById(id.Value);
            if(department == null)
                return NotFound();
            return View(ViewName,department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id == null)
            //    return BadRequest();//status code 400 client error

            //var department = _departRepo.GetById(id.Value);
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
                    _departRepo.Update(department);
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




    }
}
