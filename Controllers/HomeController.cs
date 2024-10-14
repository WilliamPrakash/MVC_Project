using Microsoft.AspNetCore.Mvc;
using MVC_Project.DAL;
using MVC_Project.Models;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MVC_ProjectDBContext _dbContext;

        public HomeController(ILogger<HomeController> logger, MVC_ProjectDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        // Each of these corresponds to a single view (Controller can use multiple views)
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            return View(); // returns the view with the same name as the method
        }

        public IActionResult CreateEditExpense()
        {
            return View();
        }

        // A model of type expense (or object) gets submitted here
        public IActionResult CreateEditExpenseForm(Expense model)
        {
            return RedirectToAction("Expenses");
        }

        public void Sql_Conn()
        {
            SqlConnection conn = new SqlConnection();
            //can you swap "Desktop-VV7FH87" for "localhost" ? yes
            conn.ConnectionString = "Server=Desktop-VV7FH87;Database=master;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
            string query = "select * from dbo.Contacts";
            conn.Open();
            var command = new SqlCommand(query, conn);
            var reader = command.ExecuteReader();

            while(reader.Read())
            {
                Console.WriteLine("{0} {1} {2} {3}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
            }
            
            Console.WriteLine("");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
