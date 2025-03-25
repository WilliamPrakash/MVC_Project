using Microsoft.AspNetCore.Mvc;
using MVC_Project.DAL;
using MVC_Project.Models;
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

        # region Views
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            using (var context = new MVC_ProjectDBContext())
            {
                var totalExpenses = context.Expenses.Sum(Expense => Expense.Value);
                ViewBag.TotalExpenses = totalExpenses;
                return View(context.Expenses.ToList()); // returns the view with the same name as the method
            }
        }

        public IActionResult CreateEditExpense(int? id) // If you create something new, you won't have an Id
        {
            if (id.HasValue) // Edit
            {
                using (var context = new MVC_ProjectDBContext())
                {
                    // Find expense to edit via id
                    var expenseToEdit = context.Expenses.SingleOrDefault(expense => expense.Id == id);
                    return View(expenseToEdit);
                }
            }
            return View();
        }
        # endregion

        public IActionResult DeleteExpense(int id)
        {
            using (var context = new MVC_ProjectDBContext())
            {
                // SingleOrDefault() takes the first record found where the record id matches the parameter (id)
                Expense? expenseToDelete = context.Expenses.SingleOrDefault(expense => expense.Id == id);
                if (expenseToDelete != null)
                {
                    context.Expenses.Remove(expenseToDelete);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Expenses");
        }

        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if (model.Id == 0) // Create Expense
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
                }
            }
            else // Edit Expense
            {
                using (var context = new MVC_ProjectDBContext())
                {
                    context.Expenses.Update(model);
                    context.SaveChanges();
                }
            }
            return RedirectToAction("Expenses");
        }

        [Obsolete("No longer using vanilla C# to access SQL, use Entity Framework instead.")]
        // Keep for reference
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
    }
}
