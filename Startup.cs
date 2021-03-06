﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TechTime.Authorization;
using TechTime.Models;
using TechTime.Service;
using TechTime.ViewModels;

namespace TechTime
{
    public class Startup
    {
        private IHostingEnvironment _env;
        private IConfigurationRoot _config;

        public Startup(IHostingEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();

            _config = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            services.AddScoped<IRecordRepository, RecordRepository>();
            services.AddDbContext<RecordContext>(ServiceLifetime.Scoped);
            services.AddTransient<DatabaseSeeder>();

            services.AddIdentity<UserLogin, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 1;
                config.Password.RequireDigit = false;
                config.Password.RequireUppercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<RecordContext>();

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
                options.AppendTrailingSlash = false;
            });

            services.AddMvc(config =>
            {
                if (_env.IsDevelopment())
                {
                    //config.Filters.Add(new RequireHttpsAttribute());
                }


            }).AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Error;
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddScoped<IAuthorizationHandler,OwnerAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler,ManagerAuthorizationHandler>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DatabaseSeeder databaseSeeder)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseIdentity();
            

            loggerFactory.AddDebug();
            loggerFactory.AddFile("Logs/techtime_{Date}.txt");

            Mapper.Initialize(config =>
            {
                config.CreateMap<JobEntryViewModel, JobEntry>();
                config.CreateMap<JobEntry, HistoryViewModel>();
            });

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
                app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
            }

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "Home", action = "Index" });
            });


            databaseSeeder.Seed().Wait();

        }
    }
}
