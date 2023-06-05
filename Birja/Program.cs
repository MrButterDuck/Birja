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
            //�������� �������
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json").Build().Bind("Project", new Config());
            Configuration = configurationBuilder.Build();

            var builder = WebApplication.CreateBuilder(args);

            //���������� ������
            builder.Services.AddControllersWithViews(opt => opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
            builder.Services.AddTransient<ICardRepositoriy, EFCardRepository>();
            builder.Services.AddTransient<DataManager>();

            //���������� ��
            builder.Services.AddDbContext<AppDbContex>(x => x.UseSqlServer(Config.ConnectionString));
            Debug.WriteLine(Config.ConnectionString);

            //��������� identity �������
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 6;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireDigit = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<AppDbContex>().AddDefaultTokenProviders();

            //����������� authentication coockie
            builder.Services.ConfigureApplicationCookie(opts =>
            {
                opts.Cookie.Name = "BirjaAuth";
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
            //��������� ������ Asp.Net Core
            builder.Services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();

            var app = builder.Build();
            //��� ��������� ���������� �� �������
            if (builder.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //����� �����!!!
            //����������� �����
            app.UseStaticFiles();
            //�������������
            app.UseRouting();
            //���������� ����������� � ������������
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();
            //������ � endPoints
            app.UseEndpoints(endpoints =>
            {
                //C�������� ��������
                endpoints.MapControllerRoute("user", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("admin", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run();
        }
    }
}
