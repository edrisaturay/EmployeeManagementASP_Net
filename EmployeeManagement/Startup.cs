using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>();
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

            //overriding the default file name
            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear(); //clear the default default file name
            //defaultFilesOptions.DefaultFileNames.Add("foo.html"); //specifying the default file to load
            //app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
               routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

            
            //app.UseMvcWithDefaultRoute();
            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
            //app.UseFileServer();

            app.Run(async (context) =>
            {
                //throw new Exception("Some error processing the request");
                await context.Response.WriteAsync("Hello from the other side");
            });

            
        }
    }
}
