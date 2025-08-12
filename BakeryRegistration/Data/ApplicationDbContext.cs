using BakeryRegistration.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace BakeryRegistration.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BakeryModel> Bakeries { get; set; }

        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
