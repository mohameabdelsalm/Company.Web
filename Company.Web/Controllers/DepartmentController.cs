using Company.Data.Entites;
using Company.Repository.Interface;
using Company.Service.Dto;
using Company.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Company.Web.Controllers
{
    //[Authorize(Roles = "Department")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        public IActionResult Index()
        {
            var departments = _departmentService.GetAll();
            return View(departments);

        }
        [HttpGet]
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentDto department)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _departmentService.Add(department);
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Department Error", "Validation errors");
                return View(department);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("Department Error", ex.Message);
                return View(department);
            }


        }

        //Page Details => Presention Details Department ById
        public IActionResult Details(int? id, string viewName = "Details")
        {
            var department = _departmentService.GetById(id);
            if (department is null)
            {
                return RedirectToAction("ErrorPage", null, "Home");
            }

            return View(viewName, department);
        }
        [HttpGet]
        public IActionResult Update(int? id)
        {

            return Details(id, "Update");
        }

        [HttpPost]
        public IActionResult Update(int? id, DepartmentDto department)
        {
            if (id == null || department.ID != id.Value)
                return RedirectToAction("Error");

            _departmentService.Update(department);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var department = _departmentService.GetById(id);
            if (department is null)
            {
                return RedirectToAction("Error");
            }

            _departmentService.Delete(department);

            return RedirectToAction(nameof(Index));
        }



    }
}









