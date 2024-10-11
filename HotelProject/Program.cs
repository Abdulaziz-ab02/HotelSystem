using HotelProject.Interfaces;
using HotelProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;

namespace HotelProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Register EmailSender as a service for dependency injection
            builder.Services.AddTransient<IEmailSender, EmailSender>();

            // Configure DbContext for Oracle database
            builder.Services.AddDbContext<ModelContext>(options =>
                    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Configure session management with a 60-minute timeout
            builder.Services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Add MVC services
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Use HTTPS redirection and serve static files
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // Enable session support
            app.UseSession();

            // Enable routing and authorization
            app.UseRouting();
            app.UseAuthorization();

            // Configure default route mapping
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            // Run the application
            app.Run();
        }
    }
}
