using BigDobaUProject.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace BigDobaUProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var connString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DobaDbContext>(options =>
                options.UseSqlServer(connString));

            // Configure the HTTP request pipeline.
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Music}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
