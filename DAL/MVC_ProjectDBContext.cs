using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace MVC_Project.DAL
{
    public class MVC_ProjectDBContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        /*public MVC_ProjectDBContext(DbContextOptions<MVC_ProjectDBContext> options) : base(options)
        {
            
        }*/
    }
}
