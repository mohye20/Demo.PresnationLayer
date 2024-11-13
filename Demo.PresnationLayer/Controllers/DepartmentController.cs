
using Demo.BuniessLogicLayer.Interfaces;
using Demo.BuniessLogicLayer.Repositories;
using Demo.DataAcessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Demo.PresnationLayer.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IDepartmentRepos _departmentRepos;

        public DepartmentController(IDepartmentRepos departmentRepos)
        {
            _departmentRepos =  departmentRepos;
        }
        public IActionResult Index()
        {

            var department = _departmentRepos.GetAll();
            return View(department);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Departmnet departmnet)
        {
            if (ModelState.IsValid) // server side validation 
            {
                _departmentRepos.Add(departmnet);

                return RedirectToAction(nameof(Index));
            }

            return View(departmnet);
        }

        public IActionResult Details(int? id)
        {
        

            if(id is null)
            {

                return BadRequest();
            }

            var department = _departmentRepos.GetById(id.Value);
            if(department is null)
            {
                return NotFound();
            }

            return View(department);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id is null)
            {
                return BadRequest();
            }

            var department = _departmentRepos.GetById(id.Value);
                if(department is null)
            {
                return NotFound();
            }
            return View(department);

        }
    }
}
