using Demo.BuniessLogicLayer.Interfaces;
using Demo.BuniessLogicLayer.Repositories;
using Demo.DataAcessLayer.Context;
using Demo.DataAcessLayer.Models;
using Demo.PresnationLayer.MappingProfiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.PresnationLayer
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var Builder = WebApplication.CreateBuilder(args);

			#region Configure Builder.Services Allow DI

			Builder.Services.AddControllersWithViews();
			Builder.Services.AddDbContext<MVCAppDbContext>(Options =>
			{
				Options.UseSqlServer(Builder.Configuration.GetConnectionString("DefaultConnection"));
			}, ServiceLifetime.Scoped); //Allow Dependacy Injection

			Builder.Services.AddScoped<IDepartmentRepos, DepartmentRepos>(); // Allow Depandacy injection class DepartmentRepos
																			 //
			Builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			Builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Option =>
			{
				Option.Password.RequireNonAlphanumeric = true;
				Option.Password.RequireDigit = true;
				Option.Password.RequireLowercase = true;
				Option.Password.RequireUppercase = true;
			})
				.AddEntityFrameworkStores<MVCAppDbContext>().AddDefaultTokenProviders();
			Builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
			//Builder.Services.AddScoped<UserManager<ApplicationUser>>();
			Builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(Options =>
				{
					Options.LoginPath = "Account/Login";
					Options.AccessDeniedPath = "Home/Error";
				});

			#endregion Configure Builder.Services Allow DI

			var app = Builder.Build();
			#region Configure Https Request PipeLines
			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Account}/{action=Login}/{id?}");
			});
			#endregion
			app.Run();
		}
	}
}