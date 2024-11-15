using Demo.BuniessLogicLayer.Interfaces;
using Demo.BuniessLogicLayer.Repositories;
using Demo.DataAcessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.PresnationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepos _employeeRepos;

        public EmployeeController(IEmployeeRepos employeeRepos) // Ask CLR For Creating Object From Class Impiliment Interface IEmployeeRepos
        {
            _employeeRepos = employeeRepos;
        }
        public IActionResult Index()
        {
            var Employees = _employeeRepos.GetAll();
            // 1. viewData => KeyValueOaure[Dicionary Object ] 
            //Transefer Data From Controller [Action] To Its View
            // .Net FrameWork 3.5 
            //ViewData["Message"] = "Hello From View Data";

            //2. ViewBag = Dynamic Property [Based om Dynamic Keyword]
            //Transefer Data From Controller [Action] To Its View
            // .Net FrameWork 4.0
            // 
            //ViewBag.Message = "Hello From View Bag";

			return View(Employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid) // server side validation
            {
                _employeeRepos.Add(employee);

                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }

            var employee = _employeeRepos.Get(id.Value);
            if (employee is null)
            {
                return NotFound();
            }

            return View(ViewName, employee);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
           

            return Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee, [FromRoute] int id)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepos.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError(string.Empty, Ex.Message);
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
        public IActionResult Delete(Employee employee, [FromRoute] int id)
        {
            if (employee.Id != id)
            {
                return BadRequest();
            }
            try
            {
                _employeeRepos.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(string.Empty, Ex.Message);
                return View(employee);
            }


        }


    }
}
