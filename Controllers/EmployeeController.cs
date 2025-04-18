using MVC_Project.Models;
using MVC_Project.DAL;
using Microsoft.AspNetCore.Mvc;


namespace MVC_Project.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly MVC_ProjectDBContext _dbContext;
        
        public EmployeeController(MVC_ProjectDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Views
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employees()
        {
            List<Employee> employees = new List<Employee>();
            using (_dbContext)
            {
                employees = _dbContext.Employees.ToList();
                ViewBag.Employees = employees;
                return View(employees);
            }
        }

        public IActionResult EditEmployee(int id)
        {
            using (_dbContext)
            {
                Employee employeeToEdit = _dbContext.Employees.Single(employee => employee.Id == id);
                return View(employeeToEdit); // returns the view with the same name as the method
            }
        }
        #endregion

        #region Logic
        public IActionResult EditEmployeeRecord(Employee updatedEmployee)
        {
            using (_dbContext)
            {
                _dbContext.Employees.Update(updatedEmployee);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Employees");
        }
        
        
        #endregion

    }
}
