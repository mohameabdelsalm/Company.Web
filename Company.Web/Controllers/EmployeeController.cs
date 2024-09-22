using Company.Data.Entites;
using Company.Service.Interface;
using Company.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
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
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeService.Add(employee);
                    return RedirectToAction();
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
