using Company.Data.Entites;
using Company.Repository.Interface;
using Company.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Company.Web.Controllers
{
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
        public IActionResult Create(Department department)
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
        public IActionResult Details(int? id)
        {
            var department = _departmentService.GetById(id);
            if (department is null)
            {
               return RedirectToAction("ErrorPage",null,"Home");
            }
           
            return View(department);
        }



    }
}









