using Company.Data.Entites;
using Company.Service.Dto;
using Company.Service.Interface;
using Company.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService,IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
        }
        public IActionResult Index(string searchInp)
        {
            if (string.IsNullOrEmpty(searchInp))
            {
                var employees = _employeeService.GetAll();
                return View(employees);
            }
            else
            {
                var employees = _employeeService.GetEmployeeByName(searchInp);
                return View(employees);
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.Add(employee);
                    return RedirectToAction(nameof(Index));
                }
           
                return View(employee);
            }

            catch (Exception ex)
            {
               
                return View(employee);
            }

        }
        public IActionResult Update()
        {
            return View();
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
