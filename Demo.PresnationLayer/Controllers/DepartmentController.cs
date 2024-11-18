using Demo.BuniessLogicLayer.Interfaces;
using Demo.BuniessLogicLayer.Repositories;
using Demo.DataAcessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace Demo.PresnationLayer.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var department = _unitOfWork.DepartmentRepos.GetAll();
            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department departmnet)
        {
            if (ModelState.IsValid) // server side validation
            {
                _unitOfWork.DepartmentRepos.Add(departmnet);
              int Result =   _unitOfWork.Compelete();
              
                if(Result > 0) { 
                TempData["Message"] = "Department Is Created";
                }
                return RedirectToAction(nameof(Index));
            }

            return View(departmnet);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
            {
                return BadRequest();
            }

            var department = _unitOfWork.DepartmentRepos.Get(id.Value);
            if (department is null)
            {
                return NotFound();
            }

            return View(ViewName, department);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if(id is null)
            //{
            //    return BadRequest();
            //}

            //var department = _unitOfWork.DepartmentRepos.GetById(id.Value);
            //    if(department is null)
            //{
            //    return NotFound();
            //}
            //return View(department);

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department, [FromRoute] int id)
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
                    _unitOfWork.Compelete();
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
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Department department, [FromRoute] int id)
        {
            if (department.Id != id)
            {
                return BadRequest();
            }
            try
            {
                _unitOfWork.DepartmentRepos.Delete(department);
                _unitOfWork.Compelete();
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