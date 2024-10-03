using AutoMapper;
using Demo.BLL.Interface;
using Demo.DAL.Models;
using DemoPL.Helpers;
using DemoPL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace DemoPL.Controllers
{
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
        public IActionResult Index(string searchValue)
        {
            IEnumerable<Employee> employee;

            if (string.IsNullOrEmpty(searchValue))
                employee = _unitOfWork.EmployeeRepository.GetAll();

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
        public IActionResult Create(EmployeeViewModel employeeVM)
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

               _unitOfWork.EmployeeRepository.Add(MappedEmployee);
                _unitOfWork.Complete();



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


        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return BadRequest();//status code 400 client error

            var employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
            if (employee == null)
                return NotFound();
            var MappedEmplyee=_mapper.Map<Employee, EmployeeViewModel>(employee);   
            return View(ViewName, MappedEmplyee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id == null)
            //    return BadRequest();//status code 400 client error

            //var Employee = _departRepo.GetById(id.Value);
            //if (Employee == null)
            //    return NotFound();
            //return View(Employee);
            return Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
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
                    _unitOfWork.Complete();

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
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();
            
                try
                {
                    var mappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Delete(mappedEmployee);
                  var result=  _unitOfWork.Complete();
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
