using MassRabit.Models;
using Microsoft.EntityFrameworkCore;

namespace MassRabit.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<TestModel> TestModels { get; set; }
    }
}
