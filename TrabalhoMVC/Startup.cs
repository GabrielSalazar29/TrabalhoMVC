using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TrabalhoMVC.Data;
using TrabalhoMVC.Services;

namespace TrabalhoMVC
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();

		    services.AddDbContext<TrabalhoMVCContext>(options =>
		            options.UseMySql(Configuration.GetConnectionString("TrabalhoMVCContext"), builder =>
					builder.MigrationsAssembly("TrabalhoMVC")));

			services.AddScoped<SeedingService>();
			services.AddScoped<VendedorService>();
			services.AddScoped<DepartamentoService>();
			services.AddScoped<RegistroVendasService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SeedingService seedingService)
		{
			var br = new CultureInfo("pt-BR");
			var localizationOptions = new RequestLocalizationOptions {
				DefaultRequestCulture = new RequestCulture(br),
				SupportedCultures = new List<CultureInfo> { br },
				SupportedUICultures = new List<CultureInfo> { br }

			};

			app.UseRequestLocalization(localizationOptions);

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				seedingService.Seed();
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

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
