using MVC_Project.DAL;
using MVC_Project.Models;
using Microsoft.AspNetCore.Mvc;


namespace MVC_Project.Controllers
{
    public class ExpenseController : Controller
    {
        private readonly MVC_ProjectDBContext _dbContext;

        public ExpenseController(MVC_ProjectDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        # region Views
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            using (_dbContext)
            {
                var totalExpenses = _dbContext.Expenses.Sum(Expense => Expense.Value);
                ViewBag.TotalExpenses = totalExpenses;
                return View(_dbContext.Expenses.ToList()); // returns the view with the same name as the method
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
            using (_dbContext)
            {
                // SingleOrDefault() takes the first record found where the record id matches the parameter (id)
                Expense? expenseToDelete = _dbContext.Expenses.SingleOrDefault(expense => expense.Id == id);
                if (expenseToDelete != null)
                {
                    _dbContext.Expenses.Remove(expenseToDelete);
                    _dbContext.SaveChanges();
                }
            }
            return RedirectToAction("Expenses");
        }

        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if (model.Id == 0) // Create Expense
            {
                using (_dbContext)
                {
                    var expense = new Expense()
                    {
                        Value = model.Value,
                        Description = model.Description
                    };
                    _dbContext.Expenses.Add(expense);
                    _dbContext.SaveChanges();
                }
            }
            else // Edit Expense
            {
                using (_dbContext)
                {
                    _dbContext.Expenses.Update(model);
                    _dbContext.SaveChanges();
                }
            }
            return RedirectToAction("Expenses");
        }

    }
}
