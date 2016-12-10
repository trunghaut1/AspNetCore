using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using AspNetCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AspNetCore
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            var connection = @"Server=ZERO\SQLEXPRESS;Database=Database;Trusted_Connection=True;";
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(
                                    Configuration["Data:DatabaseIdentity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>()
                                    .AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddScoped<IUserRepository, EFUserRepository>();
            services.AddScoped<ICatRepository, EFCatRepository>();
            services.AddScoped<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IOrderRepository, EFOrderRepository>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseIdentity();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: null,
                    template: "Cat/{id:int}/page{page:int}",
                    defaults: new { controller = "Home", action = "List" }
                );
                routes.MapRoute(
                    name: null,
                    template: "Cat/page{page:int}",
                    defaults: new { controller = "Home", action = "List", page = 1 }
                );
                routes.MapRoute(
                    name: null,
                    template: "Cat/{id:int}",
                    defaults: new { controller = "Home", action = "List", page = 1 }
                );
                routes.MapRoute(
                    name: null,
                    template: "Search/{search}/page{page:int}",
                    defaults: new { controller = "Home", action = "Search" }
                );
                routes.MapRoute(
                    name: null,
                    template: "Search/{search}",
                    defaults: new { controller = "Home", action = "Search", page = 1 }
                );
                routes.MapRoute(
                    name: null,
                    template: "Account/page{page:int}",
                    defaults: new { controller = "Account", action = "Index" }
                );
                routes.MapRoute(
                    name: null,
                    template: "Product/{id:int?}",
                    defaults: new { controller = "Home", action = "Product"}
                );
                routes.MapRoute(name: null, template: "{controller=Home}/{action=Index}/{id:int?}");
            });
            IdentitySeedData.EnsurePopulated(app);
        }
    }
}
