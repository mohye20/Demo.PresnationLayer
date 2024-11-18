using AutoMapper;
using Demo.BuniessLogicLayer.Interfaces;
using Demo.DataAcessLayer.Models;
using Demo.PresnationLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Demo.PresnationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepos _employeeRepos;
        private readonly IDepartmentRepos _departmentRepos;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepos employeeRepos, IDepartmentRepos departmentRepos,
            IMapper mapper) // Ask CLR For Creating Object From Class Impiliment Interface IEmployeeRepos
        {
            _employeeRepos = employeeRepos;
            _departmentRepos = departmentRepos;
            _mapper = mapper;
        }

        public IActionResult Index(string SearchValue)
        {


            IEnumerable<Employee> Employees;
            if (string.IsNullOrEmpty(SearchValue))
                Employees = _employeeRepos.GetAll();


            else
                Employees = _employeeRepos.GetEmployeesByName(SearchValue);

            var MappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
            return View(MappedEmployee);

        }

        public IActionResult Create()
        {
            //ViewBag.Departments = _departmentRepos.GetAll();

            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid) // server side validation
            {
                /// Manual Mapping

                //var MappedEmployee = new Employee()
                //{
                //    Name = employeeVM.Name,
                //    Age = employeeVM.Age,
                //    Address = employeeVM.Address,
                //    PhoneNumber = employeeVM.PhoneNumber,
                //    DepartmentId = employeeVM.DepartmentId,

                //};
                //Employee employee = (Employee) employeeVM;

                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                _employeeRepos.Add(MappedEmployee);

                return RedirectToAction(nameof(Index));
            }

            return View(employeeVM);
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

            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(ViewName, MappedEmployee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {

                    var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    _employeeRepos.Update(MappedEmployee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError(string.Empty, Ex.Message);
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
        public IActionResult Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (employeeVM.Id != id)
            {
                return BadRequest();
            }
            try
            {
                var MapopedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _employeeRepos.Delete(MapopedEmployee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(string.Empty, Ex.Message);
                return View(employeeVM);
            }
        }
    }
}