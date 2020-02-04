using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinoCMS.Data;
using DinoCMS.Models.Services;
using DinoCMS.Models.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DinoCMS.Models;
using Microsoft.AspNetCore.Identity;

namespace DinoCMS
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            // IConfigurationRoot Configuration = new ConfigurationBuilder()
            //    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //    .AddJsonFile("appsettings.json")
            //    .Build();

            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            Configuration = builder.Build();
            //Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("ADMIN"));
            });

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>();

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //  .AddEntityFrameworkStores<UserDbContext>()
            //  .AddDefaultTokenProviders();

            services.AddDbContext<DinoDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("ProductionConnection")));


            services.AddDbContext<UserDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("UserDataConnection")));

            services.AddScoped<IDinoManager, DinoService>();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, UserManager<ApplicationUser> userManager, IServiceProvider service)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
           StartupDbInitializer.SeedData(service, userManager);
          
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
