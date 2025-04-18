using MVC_Project.DAL;
using MVC_Project.Models;
using Microsoft.AspNetCore.Mvc;


namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly MVC_ProjectDBContext _dbContext;

        public HomeController(MVC_ProjectDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        # region Views
        public IActionResult Index()
        {
            return View();
        }
        
        # endregion

    }
}
