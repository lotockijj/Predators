using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BattleBands.Models.ApplicationModels;

namespace BattleBands.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationPerformer> Performers {get;set;}
        public DbSet<ApplicationEvent> Events { get; set; }
        public DbSet<ApplicationVideo> Videos { get; set; }
        public DbSet<ApplicationPhoto> Photo { get; set; }
        public DbSet<ApplicationMusic> Music { get; set; }
        public DbSet<ApplicationComment> Comments { get; set; }
        public DbSet<ApplicationMessage> Message { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
