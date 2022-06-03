using DemoDataServerCore.Controllers;
using Flexmonster.DataServer.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace DemoDataServerCore
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
            services.ConfigureFlexmonsterOptions(Configuration);
            //Configure JsonIndexOptions
            services.Configure<DatasourceOptions>((options) =>
            {
                options.Indexes = new Dictionary<string, IndexOptions>();
                //public DatabaseIndexOptions(string databaseType, string connectionString, string query);

                options.Indexes.Add("custom-index", new JsonIndexOptions("data.json"));
            });

   

            
            services.AddControllersWithViews();
            //configure options from appsettings.json
            //add 
            //custom parser must be added as transient
           // services.AddTransient<IParser, CustomParser>();
            services.AddScoped<JsonIndexService>();
            services.AddScoped<ReloadService>();

            services.AddFlexmonsterApi();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
            }); 

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}");
            });
        }
    }
}