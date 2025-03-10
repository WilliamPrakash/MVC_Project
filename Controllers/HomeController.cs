using Microsoft.AspNetCore.Mvc;
using MVC_Project.DAL;
using MVC_Project.Models;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using System.Runtime.InteropServices;

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

        public void EstablishSQLConnection()
        {
            // Grab Credentials from credentials.json stored on local machine
            GrabLocalDatabaseCredentials credentialGrabber = new GrabLocalDatabaseCredentials();
            Dictionary<string,string> credentials = credentialGrabber.OpenLocalAuthFile();

            // Create SQL connection
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = credentials["SQLServer_Win"];
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                conn.ConnectionString = credentials["SQLServer_Mac"];
            }

            // Attempt to connect/query
            try
            {
                conn.Open();
                string query = "select * from master.dbo.Employees";
                var command = new SqlCommand(query, conn);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("{0} {1} {2} {3}", reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            //conn.Close();
            
            Console.WriteLine("");
        }

        public void TestEntity()
        {
            
            // This is like an abstraction of our database
            using (var context = new MVC_ProjectDBContext())
            {
                var expense = new Expense()
                {
                    //Id = 1,
                    Value = 200,
                    Description = "test"
                };

                context.Expenses.Add(expense);
                context.SaveChanges();
                // You can modify this context
                // Then you can save those changes onto the DB

                var allExpenses = context.Expenses.ToList();
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
