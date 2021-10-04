using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using covid_portal_api.api.Helpers;
using covid_portal_api.domain.Entities;
using covid_portal_api.infrastructure.Data;
using covid_portal_api.infrastructure.Repositories;
using covid_portal_api.infrastructure.Repositories_Interfaces;
using covid_portal_api.interfaces;
using covid_portal_api.services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols;
using Microsoft.OpenApi.Models;
using Serilog;

namespace covid_portal_api.api
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
            // get connection string from appsettings
            var covidConnectionString = Configuration.GetConnectionString("CovidDB");
            services.AddDbContext<CovidContext>(c => c.UseSqlServer(covidConnectionString), ServiceLifetime.Scoped);

            services.AddDbContext<CovidContext>(options =>  
            options.UseSqlServer(covidConnectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(90)));

            services.AddControllers()
                .AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddApiVersioning();

            services.AddHttpClient<ApiClient>((sp, config) =>
            {
                config.BaseAddress = new Uri(Configuration["CovidServiceBaseUrl"]);
                config.DefaultRequestHeaders.Accept.Clear();
                config.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }).ConfigurePrimaryHttpMessageHandler(() => {
                var handler = new HttpClientHandler();
                
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                return handler;
            });


            services.AddScoped<ICrud<Case>, CaseRepository>();
            services.AddScoped<ICrud<Country>, CountryRepository>();
            services.AddScoped<ICaseService, CaseService>();
            services.AddScoped<IApiService<Case>, ApiService<Case>>();
            services.AddScoped<IApiService<string>, ApiService<string>>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "covid_portal_api.api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "covid_portal_api.api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
