using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment_4_Cloud_Project.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Assignment_4_Cloud_Project
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
            //services.AddDbContext<Assignment_4_Cloud_ProjectDBContext>(options => options.UseSqlServer(
            //Configuration["Data:MedicalDB:ConnectionString"]));
            //services.AddMvc();

            services.AddControllersWithViews();

            var connection = @"Server=(localdb)\MSSQLLocalDB;Database=Medical;Trusted_Connection=True;ConnectRetryCount=0";

            services.AddDbContext<Assignment_4_Cloud_ProjectDBContext>
            (options => options.UseSqlServer(connection));

            //services.AddControllersWithViews();

            //services.AddDbContext<Assignment_4_Cloud_ProjectDBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Assignment_4_Cloud_ProjectDBContext")));

            //services.AddDbContext<Assignment_4_Cloud_SiteDBContext>(options => options.UseSqlServer(Configuration["Data:Assignment4:ConnectionString"]));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<Assignment_4_Cloud_ProjectDBContext>();
                context.Database.EnsureCreated();
            }
            if (env.IsDevelopment())
            {
                //app.UseBrowserLink();
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
