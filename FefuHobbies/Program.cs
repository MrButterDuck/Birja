using Microsoft.AspNetCore.Mvc;
using FefuHobbies.Service;
using FefuHobbies.Domain.Repositories.Abstract;
using FefuHobbies.Domain.Repositories.EntityFramework;
using FefuHobbies.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace Website
{
    public class Program
    {
        private static IConfiguration? Configuration;

        public static void Main(string[] args)
        {
            //привязка конфига
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json").Build().Bind("Project", new Config());
            Configuration = configurationBuilder.Build();

            var builder = WebApplication.CreateBuilder(args);

            //Подключаем логику
            builder.Services.AddControllersWithViews(opt => opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
            builder.Services.AddTransient<ICardRepositoriy, EFCardRepository>();
            builder.Services.AddTransient<DataManager>();

            //Подключаем БД
            builder.Services.AddDbContext<AppDbContex>(x => x.UseSqlServer(Config.ConnectionString));
            Debug.WriteLine(Config.ConnectionString);

            //настройка identity системы
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireDigit = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<AppDbContex>().AddDefaultTokenProviders();

            //настраиваем authentication coockie
            builder.Services.ConfigureApplicationCookie(opts =>
            {
                opts.Cookie.Name = "FefuHobbiesAuth";
                opts.Cookie.HttpOnly = true;
                opts.LoginPath = "/account/login";
                opts.AccessDeniedPath = "/account/accessdenied";
                opts.SlidingExpiration = true;
            });

            builder.Services.AddAuthorization(x =>
            {
                x.AddPolicy("AdminArea", policy => { policy.RequireRole("admin"); });
                x.AddPolicy("UserArea", policy => { policy.RequireRole("user"); });
            });
            builder.Services.AddControllersWithViews(x =>
            {
                x.Conventions.Add(new AdminAuthorization("Admin", "AdminArea"));
                x.Conventions.Add(new UserAuthorization("User", "UserArea"));
            });
            //Указываем версию Asp.Net Core
            builder.Services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();

            var app = builder.Build();
            //Для подробной информации об ошибках
            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //поряд ВАЖЕН!!!
            //Статические файлы
            app.UseStaticFiles();
            //Маршрутизация
            app.UseRouting();
            //Подключаем авторизацию и аунтефикацию
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            //работа с endPoints
            app.UseEndpoints(endpoints =>
            {
                //Cтартовая страница
                endpoints.MapControllerRoute("user", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}
