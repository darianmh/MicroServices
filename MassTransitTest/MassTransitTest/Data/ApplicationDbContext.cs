using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransitTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MassTransitTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<TestModel> TestModels { get; set; }
    }
}
