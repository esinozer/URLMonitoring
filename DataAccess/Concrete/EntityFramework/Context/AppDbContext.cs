using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class AppDbContext :  IdentityDbContext<AppUser, AppRole, string> 
    {
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // Add-Migration InıtialCreate -Context AppDbContext
        //update-database -context AppDbContext
        public DbSet<ScheduleSettings> ScheduleSettings { get; set; }
        public DbSet<Logs> Logs { get; set; }

    }
}
