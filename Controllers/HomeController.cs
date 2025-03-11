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

        # region View return functions
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            using (var context = new MVC_ProjectDBContext())
            {
                return View(context.Expenses.ToList()); // returns the view with the same name as the method
            }
        }

        public IActionResult CreateEditExpense()
        {
            return View();
        }
        # endregion

        // Should I make a separate function for editing?
        public IActionResult CreateEditExpenseForm(Expense model)
        {
            using (var context = new MVC_ProjectDBContext())
            {
                var expense = new Expense()
                {
                    Value = model.Value,
                    Description = model.Description
                };

                context.Expenses.Add(expense);
                context.SaveChanges();
                Console.WriteLine(context.Expenses.ToList());
            }
            return RedirectToAction("Expenses");
        }

        /* Should I remove this now that I'm using EF Core to access SQL? */
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
