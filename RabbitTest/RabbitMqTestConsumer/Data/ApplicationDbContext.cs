using Microsoft.EntityFrameworkCore;
using RabbitMqTestConsumer.Models;

namespace RabbitMqTestConsumer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<TestModel> TestModels { get; set; }
    }
}
