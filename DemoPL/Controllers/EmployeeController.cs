using Demo.BLL.Interface;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace DemoPL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmpolyeeRepository _empolyeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmpolyeeRepository empolyeeRepository,IDepartmentRepository departmentRepository)
        {
            this._empolyeeRepository = empolyeeRepository;
            this._departmentRepository = departmentRepository;
        }
        public IActionResult Index(string searchValue)
        {
            IEnumerable<Employee> employee;
            if (string.IsNullOrEmpty(searchValue))
            {
                employee = _empolyeeRepository.GetAll();

            }
            else
            {
                 employee = _empolyeeRepository.GetEmployeesByName(searchValue);

            }
            return View(employee);

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
        }

        public IActionResult Create()
        {
          ViewBag.Departments =_departmentRepository.GetAll();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid) //server side 
            {

               int result= _empolyeeRepository.Add(employee);
                if (result > 0)
                {
                    TempData["message"] = "Employee is created";

                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }


        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return BadRequest();//status code 400 client error

            var employee = _empolyeeRepository.GetById(id.Value);
            if (employee == null)
                return NotFound();
            return View(ViewName, employee);
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
        public IActionResult Edit(Employee employee, [FromRoute] int id)
        {
            if (id != employee.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _empolyeeRepository.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    //1-log excep
                    //2-form
                    ModelState.AddModelError(string.Empty, ex.Message);

                }


            }
            return View(employee);

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Employee employee, [FromRoute] int id)
        {
            if (id != employee.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _empolyeeRepository.Delete(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    //1-log excep
                    //2-form
                    ModelState.AddModelError(string.Empty, ex.Message);

                }


            }
            return View(employee);

        }

    }
}
