using AutoMapper;
using Demo.BLL.Interface;
using Demo.DAL.Models;
using DemoPL.Helpers;
using DemoPL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoPL.Controllers
{
	[Authorize]

	public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork//ASK CLR FOR OBJECT FROM CLASS IMPLEMENT INTERFACE
            , IMapper mapper)
        {
           
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchValue)
        {
            IEnumerable<Employee> employee;

            if (string.IsNullOrEmpty(searchValue))
                employee =await _unitOfWork.EmployeeRepository.GetAllAsync();

            else
                employee = _unitOfWork.EmployeeRepository.GetEmployeesByName(searchValue);
            var MappedEmplyee = _mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employee);


            return View(MappedEmplyee);
        }

            //1-view data =>keyvaluepair[dictionary object]
            //transfer data from controller [action] to its view
            //.net framework 3.5
            //faster
            //  ViewData["message"] = "Hello from view data";
            //2.viewbag=>dynamic property [based on dynamic keyword]
            //data type in run time
            //no casting
            //transfer data
            //.net 4.8
            //  ViewBag.message = "hello from view bag";
        

        public IActionResult Create()
        {
          //ViewBag.Departments =_departmentRepository.GetAll();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid) //server side 
            {
                #region manual mapping

                ////var MappingEmployee = new Employee()
                ////{
                ////    Name = employeeVM.Name,
                ////    age =employeeVM.age
                ////};
                ///  Employee employee = (Employee)employeeVM;
                ///   
                #endregion


               employeeVM.ImageName=      DocumentSetting.UploadFile(employeeVM.Image, "Images");

                var MappedEmployee=  _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

             await  _unitOfWork.EmployeeRepository.AddAsync(MappedEmployee);
               await _unitOfWork.CompleteAsync();



                //dbcontext.dispose();

                //1.updata
                //2.delete
                //3.updata
                //(locally)
                //_dbcontext.set<t>().add();
                //_dbcontext.savechanges();  

                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }


        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return BadRequest();//status code 400 client error

            var employee =await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if (employee == null)
                return NotFound();
            var MappedEmplyee=_mapper.Map<Employee, EmployeeViewModel>(employee);   
            return View(ViewName, MappedEmplyee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            //if (id == null)
            //    return BadRequest();//status code 400 client error

            //var Employee = _departRepo.GetById(id.Value);
            //if (Employee == null)
            //    return NotFound();
            //return View(Employee);
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    if(employeeVM.Image != null) 
                    {
                        employeeVM.ImageName = DocumentSetting.UploadFile(employeeVM.Image, "Images");

                    }
                    var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Update(mappedEmployee);
                   await _unitOfWork.CompleteAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    //1-log excep
                    //2-form
                    ModelState.AddModelError(string.Empty, ex.Message);

                }


            }
            return View(employeeVM);

        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            
                try
                {
                    var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Delete(mappedEmployee);
                  var result= await _unitOfWork.CompleteAsync();
                    if (result > 0&&employeeVM.ImageName is not null)
                    {
                        DocumentSetting.DeleteFile(employeeVM.ImageName,"Images");
                    }

                    return RedirectToAction(nameof(Index));
                }

                catch (System.Exception ex)
                {
                    //1-log excep
                    //2-form
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(employeeVM);

                }


            

        }

    }
}
