using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UndergroundSound.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.HttpOverrides;
using UndergroundSound.Models.UndergroundSound.Models;

namespace UndergroundSound
{
    public class Startup
    {
       public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }




        public void ConfigureServices(IServiceCollection services)
        {

            //


            //
            //services.AddTransient<ImageAdder, ImageAdderBand>();
            services.AddTransient<IFileAdder, FileAdder>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IRepository, Repository>();
            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();
           
           

            services.AddMvc();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,AppIdentityDbContext context)
        {
            app.UseStaticFiles();

            //
            if (env.IsDevelopment())
            {
                app.UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor,

                    // IIS is also tagging a X-Forwarded-For header on, so we need to increase this limit, 
                    // otherwise the X-Forwarded-For we are passing along from the browser will be ignored
                    ForwardLimit = 2
                });

                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //




            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseMvc(routes=> { routes.MapRoute(name: "default", template: "{controller}/{action}/{id}", defaults: new { action = "Index" }); });
            SeedData.EnsurePopulated(context);
        }
    }
}
