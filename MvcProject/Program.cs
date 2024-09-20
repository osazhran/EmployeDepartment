using AspNetCoreHero.ToastNotification;
using AspNetCoreHero.ToastNotification.Extensions;
using BLL.Interfaces;
using BLL.Repositories;
using DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace MvcProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            #region Add dependancy  injection for AppDbContext and connection string 
            builder.Services.AddDbContext<AppDbContext>(options =>
               {
                   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
               });
            #endregion

            #region enable dependancy injection for employeeRepository
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

            #endregion

            #region enable dependancy injection for EmployeeRepository
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            #endregion

            #region Register notification services
            builder.Services.AddNotyf(config =>
               {
                   config.DurationInSeconds = 10;
                   config.IsDismissable = true;
                   config.Position = NotyfPosition.BottomRight;
               } 
               #endregion
   );

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseNotyf();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
