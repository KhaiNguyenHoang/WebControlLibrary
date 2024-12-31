using Microsoft.EntityFrameworkCore;
using WebControlLibrary.Interfaces;
using WebControlLibrary.Services;
using WebControlLibrary.Models.Entites;

namespace WebControlLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<LibraryDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDbConnection"))); // Use connection string

            builder.Services.AddScoped<IBookService, BookService>(); // Register BookService
            builder.Services.AddScoped<IReaderService, ReaderService>(); // Register ReaderService
            builder.Services.AddScoped<IBorrowingService, BorrowingService>(); // Register BorrowingService
            builder.Services.AddControllersWithViews(); // Add MVC services

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"); // Default route for BookController

            app.Run();
        }
    }
}