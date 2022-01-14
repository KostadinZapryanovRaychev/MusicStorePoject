using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MvcMusicStoreWebProject.Data;
using MvcMusicStoreWebProject.Data.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMusicStoreWebProject
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
            string mySqlConnectionStr = Configuration.GetConnectionString("Connection");

            services.AddScoped<IRepository, Repository>();
            services.AddScoped<IDisciplineRepository, DisciplineRepository>();
            services.AddScoped<ISemesterRepository, SemesterRepository>();

            services.AddIdentity<ApplicationUser, ApplicationUserRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                // Identity frameworkStores relatva IdentityDbContexta i Identityto kato cqlo sus nashiq DbContext i nie tochno s tva pokazvame kade da se sahranqva tova Identity <MusicStoreDbContext>
            }).AddEntityFrameworkStores<MusicStoreDbContext>()
            .AddDefaultUI(); //Това ми трябва за правилното пренасочване към login/register page

            services.AddScoped<UserManagerExt>();

            services.AddDbContext<MusicStoreDbContext>(options=> options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));
            services.AddControllersWithViews();

            services.AddHttpClient();
            services.AddRazorPages();
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
                    // tuk beshe MusicStore kato controller default zadaden
                    pattern: "{controller=Attendance}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
