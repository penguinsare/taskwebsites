using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskWebsites.Models;

namespace TaskWebsites.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly DatabaseOptions _configuration;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<DatabaseOptions> config)
            : base(options)
        {
            _configuration = config.Value;
        }

        public DbSet<WebsiteCredentials> WebsiteCredentials { get; set; }
        public DbSet<Website> Websites { get; set; }
        public DbSet<WebsiteCategory> Categories { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<WebsiteCategory>()
                .HasAlternateKey(wc => wc.Name);

            builder.Entity<WebsiteCategory>()
                .HasData(
                    new WebsiteCategory() { Id = 1, Name = "Automotive" },
                    new WebsiteCategory() { Id = 2, Name = "Banking" },
                    new WebsiteCategory() { Id = 3, Name = "Consumer" },
                    new WebsiteCategory() { Id = 4, Name = "Education" },
                    new WebsiteCategory() { Id = 5, Name = "Engineering" },
                    new WebsiteCategory() { Id = 6, Name = "Energy" },
                    new WebsiteCategory() { Id = 7, Name = "Fashion" },
                    new WebsiteCategory() { Id = 8, Name = "Financial" },
                    new WebsiteCategory() { Id = 9, Name = "Food and beverage" },
                    new WebsiteCategory() { Id = 10, Name = "Healthcare" },
                    new WebsiteCategory() { Id = 11, Name = "Insurance" },
                    new WebsiteCategory() { Id = 12, Name = "Legal" },
                    new WebsiteCategory() { Id = 13, Name = "Manufacturing" },
                    new WebsiteCategory() { Id = 14, Name = "Media" },
                    new WebsiteCategory() { Id = 15, Name = "Real estate" },
                    new WebsiteCategory() { Id = 17, Name = "Raw materials" },
                    new WebsiteCategory() { Id = 18, Name = "Religion" },
                    new WebsiteCategory() { Id = 19, Name = "Retail" },
                    new WebsiteCategory() { Id = 20, Name = "Jewelry" },
                    new WebsiteCategory() { Id = 21, Name = "Technology" },
                    new WebsiteCategory() { Id = 22, Name = "Telecommunications" },
                    new WebsiteCategory() { Id = 23, Name = "Government" },
                    new WebsiteCategory() { Id = 24, Name = "Transportation" },
                    new WebsiteCategory() { Id = 25, Name = "Electronics" },
                    new WebsiteCategory() { Id = 26, Name = "Non-profit" });

        }
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {

        ApplicationDbContext IDesignTimeDbContextFactory<ApplicationDbContext>.CreateDbContext(string[] args)
        {

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var dbOptions = new DatabaseOptions();            
            dbOptions.ConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=websites;Integrated Security=true;AttachDbFileName=C:\\Users\\shide\\source\\repos\\TaskWebsites\\websites.mdf";

            return new ApplicationDbContext(optionsBuilder.Options, Options.Create<DatabaseOptions>(dbOptions));
        }
    }

}
