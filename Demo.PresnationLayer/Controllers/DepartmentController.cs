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
        private readonly IDepartmentRepos _departmentRepos;

        public DepartmentController(IDepartmentRepos departmentRepos)
        {
            _departmentRepos = departmentRepos;
        }

        public IActionResult Index()
        {
            var department = _departmentRepos.GetAll();
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
               int Result =  _departmentRepos.Add(departmnet);
                // 1. Temp Data => Dicitionary object 
                // Transfer Data From Action To Action 
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

            var department = _departmentRepos.Get(id.Value);
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

            //var department = _departmentRepos.GetById(id.Value);
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
                    _departmentRepos.Update(department);
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
                _departmentRepos.Delete(department);
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