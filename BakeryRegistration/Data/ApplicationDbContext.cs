using BakeryRegistration.Models;
using Microsoft.EntityFrameworkCore;
using BakeryRegistration.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace BakeryRegistration.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserModel>
    {
        public DbSet<BakeryModel> Bakeries { get; set; }
        public DbSet<UserModel> Users { get; set; }
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
