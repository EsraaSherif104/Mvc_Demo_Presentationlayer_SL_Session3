using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.DAL.Contexts;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Demo.BLL.Repositories;
using Demo.BLL.Interface;
using DemoPL.MappingProfile;
using Microsoft.AspNetCore.Identity;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Authentication.Cookies;


namespace DemoPL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<MvcAppDbcontext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            },ServiceLifetime.Scoped);
            //allow depandany injection
            //Life time of object
            //per request(addscoped) when request stop whill remove object
            //application run (singelton)//all time you run app
            //object per operation lifetime(transient)
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddAutoMapper(m=>m.AddProfile(new EmployeeProfile()));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                Options.Password.RequireNonAlphanumeric = true;
                Options.Password.RequireDigit = true;
                Options.Password.RequireLowercase = true;
                Options.Password.RequireUppercase = true;
                //p@ssw0rd
                //Pa$$w0rd
            })


                .AddEntityFrameworkStores<MvcAppDbcontext>()
                .AddDefaultTokenProviders();
                
          
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(Options=>
            {
                Options.LoginPath = "Account/Login";
                Options.AccessDeniedPath = "Home/Error";
            });
           
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
