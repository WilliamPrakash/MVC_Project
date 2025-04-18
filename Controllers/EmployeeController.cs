//using System;
//using System.Collections.Generic;
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
        #endregion



    }
}
