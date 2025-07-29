using Bookly.Data;
using Bookly.Service.Abstract;
using Bookly.Service.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Bookly.WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = ".Bookly.Session";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.IOTimeout = TimeSpan.FromMinutes(10);
            });

            builder.Services.AddDbContext<DatabaseContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.LoginPath = "/Account/SignIn";
                    x.AccessDeniedPath = "/AccessDenied";
                    x.Cookie.Name = "Account";
                    x.Cookie.MaxAge = TimeSpan.FromDays(7);
                    x.Cookie.IsEssential = true;
                });

            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
                x.AddPolicy("UserPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "Admin", "User", "Customer"));
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "admin",
                pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
