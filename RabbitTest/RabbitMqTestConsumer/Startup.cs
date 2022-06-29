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
using Microsoft.EntityFrameworkCore;
using Nest;
using RabbitMqTestConsumer.Class;
using RabbitMqTestConsumer.Data;

namespace RabbitMqTestConsumer
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
            //db
            string connectionString = Configuration.GetConnectionString("DevelopmentDefaultConnection");
            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(connectionString));

            services.AddTransient<ApplicationDbContext>();
            services.AddTransient<Consumer>();
            services.AddControllersWithViews();
            AddElastic(ref services);
        }



        private void AddElastic(ref IServiceCollection services)
        {
            var settings = new ConnectionSettings()
                .DefaultMappingFor<ElkLogModel>(x => x.IndexName("rabbitlog"));
            var client = new ElasticClient(settings);
            services.AddSingleton(client);
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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
