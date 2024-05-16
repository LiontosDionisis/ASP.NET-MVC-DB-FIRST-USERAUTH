using Microsoft.EntityFrameworkCore;
using TeachersMVC.Data;

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
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
