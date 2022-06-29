using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MassRabit.Classes;
using MassRabit.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace MassRabit
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
            //mass transit
            services.AddMassTransit(x =>
            {
                var ass = Assembly.GetEntryAssembly();
                x.AddConsumers(ass);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username("admin");
                        h.Password("Aa1234");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
            services.AddOptions<MassTransitHostOptions>()
                .Configure(options =>
                {

                    options.WaitUntilStarted = true;

                    options.StartTimeout = TimeSpan.FromSeconds(10);

                    options.StopTimeout = TimeSpan.FromSeconds(30);
                });


            //services
            services.AddTransient<ApplicationDbContext>();
            services.AddTransient<TestPublisher>();
            services.AddTransient<TestConsumer>();
            //db
            string connectionString = Configuration.GetConnectionString("DevelopmentDefaultConnection");
            services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(connectionString));
            services.AddControllersWithViews();
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
