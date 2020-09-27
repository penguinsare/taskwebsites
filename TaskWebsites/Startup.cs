using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TaskWebsites.Data;
using TaskWebsites.Services;

namespace TaskWebsites
{
    public class Startup
    {
        private IConfiguration _configuration;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
        }
      

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ApplicationDbContext>();
            services.Configure<DatabaseOptions>(_configuration.GetSection(DatabaseOptions.SectionName));

            services.Configure<SaveHomepageSnapshotOptions>(_configuration.GetSection(SaveHomepageSnapshotOptions.SectionName));
            services.AddSingleton<ISaveHomepageSnapshotToDiskHandler, SaveHomepageSnapshotToDiskHandler>();
            services.AddSingleton<IPasswordSafe, PasswordSafe>(ps => new PasswordSafe(Environment.GetEnvironmentVariable("ENCRYPTION_KEY")));            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/error-during-development");
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(env.ContentRootPath, _configuration.GetSection("HomepageSnapshotsFolder:RelativePath").Value)),
                RequestPath = _configuration.GetSection("HomepageSnapshotsFolder:RelativeUrlPath").Value
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
