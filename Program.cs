using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TeachersMVC.Data;
using TeachersMVC.Repositories;

namespace TeachersMVC
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);
			var connString = builder.Configuration.GetConnectionString("DefaultConnection");
			
			builder.Services.AddDbContext<TeachersMvcdbContext>(options => options.UseSqlServer(connString));
			builder.Services.AddControllersWithViews();
			builder.Services.AddRepositories();
			builder.Services.AddControllersWithViews();

			builder.Services.AddAuthentication(
				CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
				{
					option.LoginPath = "/User/Login";
					option.ExpireTimeSpan = TimeSpan.FromMinutes(30);
				});

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

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=User}/{action=Login}/{id?}");

			app.Run();
		}
	}
}
