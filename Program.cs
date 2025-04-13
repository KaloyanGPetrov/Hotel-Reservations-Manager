using Hotel_Reservations_Manager.Data;
using Hotel_Reservations_Manager.Data.Entities;
using Hotel_Reservations_Manager.Data.Seeder;
using Hotel_Reservations_Manager.Repositories;
using Hotel_Reservations_Manager.Repositories.Abstraction;
using Hotel_Reservations_Manager.Services;
using Hotel_Reservations_Manager.Services.Abstraction;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hotel_Reservations_Manager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.UseLazyLoadingProxies();
            });
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews(); 

            builder.Services.AddScoped<IRoomRepository, RoomRepository>();
            builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
            builder.Services.AddScoped<IClientRepository, ClientRepository>();

            builder.Services.AddScoped<IRoomServices, RoomServices>();
            builder.Services.AddScoped<IReservationServices, ReservationServices>();
            builder.Services.AddScoped<IClientServices, ClientServices>();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                UserSeeder.Initialize(services).Wait();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
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
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
