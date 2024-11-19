using AutoMapper;
using Demo.BuniessLogicLayer.Interfaces;
using Demo.DataAcessLayer.Models;
using Demo.PresnationLayer.Helpers;
using Demo.PresnationLayer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.PresnationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, // ASK CLR For Object From Class Implememnt Interface UnitOfWork
            IMapper mapper) // Ask CLR For Creating Object From Class Impiliment Interface IEmployeeRepos
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> Employees;
            if (string.IsNullOrEmpty(SearchValue))
                Employees =await _unitOfWork.EmployeeRepos.GetAllAsync();
            else
                Employees = _unitOfWork.EmployeeRepos.GetEmployeesByName(SearchValue);

            var MappedEmployee = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(Employees);
            return View(MappedEmployee);
        }

        public IActionResult Create()
        {
            //ViewBag.Departments = _departmentRepos.GetAll();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (ModelState.IsValid) // server side validation
            {
                employeeVM.ImageName = DocumentSettings.UploadFile(employeeVM.Image, "Images");
                var MappedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

               await _unitOfWork.EmployeeRepos.AddAsync(MappedEmployee);

               await _unitOfWork.CompeleteAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(employeeVM);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }

            var employee = await _unitOfWork.EmployeeRepos.GetAsync(id.Value);
            if (employee is null)
            {
                return NotFound();
            }

            var MappedEmployee = _mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(ViewName, MappedEmployee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
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
                    _unitOfWork.EmployeeRepos.Update(MappedEmployee);
                    await _unitOfWork.CompeleteAsync();

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
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (employeeVM.Id != id)
            {
                return BadRequest();
            }
            try
            {
                var MapopedEmployee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                _unitOfWork.EmployeeRepos.Delete(MapopedEmployee);
                var Result = await _unitOfWork.CompeleteAsync();
                if (Result > 0 && employeeVM.ImageName is not null)
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");

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