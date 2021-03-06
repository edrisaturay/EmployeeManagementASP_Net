﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDBContext>(
                options => options.UseSqlServer(
                    _config.GetConnectionString(
                        "DefaultConnection"
                    )
                )
            );
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 2;
            })
                .AddEntityFrameworkStores<AppDBContext>();
            //services.Configure<IdentityOptions>(options =>
            //{
                //options.Password.RequiredLength = 8;
                //options.Password.RequiredUniqueChars = 2;
            //});
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlSerializerFormatters();
            services.AddScoped<IEmployeeRepository, SQLEmployeeRepository>();
            //services.AddTransient<IEmployeeRepository, MockEmployeeRepository>();
            //services.AddScoped<IEmployeeRepository, MockEmployeeRepository>();
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
            //services.AddMvcCore();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions();
                developerExceptionPageOptions.SourceCodeLineCount = 3;

                app.UseDeveloperExceptionPage(developerExceptionPageOptions);
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            //overriding the default file name
            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear(); //clear the default default file name
            //defaultFilesOptions.DefaultFileNames.Add("foo.html"); //specifying the default file to load
            //app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
               routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            
            //app.UseMvcWithDefaultRoute();
            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
            //app.UseFileServer();

            //app.Run(async (context) =>
            //{
                //throw new Exception("Some error processing the request");
              //m  await context.Response.WriteAsync("Hello from the other side");
            //});

            
        }
    }
}
