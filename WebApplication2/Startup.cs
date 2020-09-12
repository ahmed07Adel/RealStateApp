using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace WebApplication2
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
            services.AddIdentity<AppUsers, IdentityRole>(
                options => {
                    options.Password.RequiredLength = 6;


                }).AddEntityFrameworkStores<Appdbcontext>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));

                
            }).AddXmlDataContractSerializerFormatters();
            services.AddScoped< IEmployeeRepository, SQLemployeeresporitory > ();
           
            services.AddMvc();
            var x = _config.GetConnectionString("employeedbconnection");
            services.AddDbContext<Appdbcontext>(
                options => options.UseSqlServer(_config.GetConnectionString("employeedbconnection")));
           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes => {

                routes.MapRoute("default","{controller=home}/{action=Index}/{id?}");

            });
        }
        
        }
    }

