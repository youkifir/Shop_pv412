using Microsoft.EntityFrameworkCore;
using Shop_pv412.Services;

namespace Shop_pv412
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ShopContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddScoped<IServiceProduct, ServiceProduct>();
            builder.Services.AddControllersWithViews();
            var app = builder.Build();

            app.UseRouting();
            app.UseStaticFiles();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}