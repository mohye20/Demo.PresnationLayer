using Demo.BuniessLogicLayer.Interfaces;
using Demo.BuniessLogicLayer.Repositories;
using Demo.DataAcessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Threading.Tasks;

namespace Demo.PresnationLayer.Controllers
{

    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            //var department = _unitOfWork.DepartmentRepos.GetAllAsync().Result;
            var department =await _unitOfWork.DepartmentRepos.GetAllAsync();
            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department departmnet)
        {
            if (ModelState.IsValid) // server side validation
            {
               await _unitOfWork.DepartmentRepos.AddAsync(departmnet);
              int Result = await  _unitOfWork.CompeleteAsync();
              
                if(Result > 0) { 
                TempData["Message"] = "Department Is Created";
                }
                return RedirectToAction(nameof(Index));
            }

            return View(departmnet);
        }

        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }

            var department =await _unitOfWork.DepartmentRepos.GetAsync(id.Value);
            if (department is null)
            {
                return NotFound();
            }

            return View(ViewName, department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            

            return  await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Department department, [FromRoute] int id)
        {
            if (id != department.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.DepartmentRepos.Update(department);
                   await _unitOfWork.CompeleteAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception Ex)
                {
                    ModelState.AddModelError(string.Empty, Ex.Message);
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
        public async Task<IActionResult> Delete(Department department, [FromRoute] int id)
        {
            if (department.Id != id)
            {
                return BadRequest();
            }
            try
            {
                _unitOfWork.DepartmentRepos.Delete(department);
               await _unitOfWork.CompeleteAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception Ex)
            {
                ModelState.AddModelError(string.Empty,Ex.Message);
                return View(department);
            }


        }
    }
}