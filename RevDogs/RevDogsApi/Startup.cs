using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RevDogsApi.Models;
using Microsoft.OpenApi.Models;
using RevDogs.Core.Interfaces;
using RevDogs.DataAccess.Repository;
using Microsoft.AspNetCore.Mvc.Formatters;
using RevDogs.DataAccess.Model;

namespace RevDogsApi
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
            services.AddApplicationInsightsTelemetry();

            services.AddDbContext<RevatureDogsP2Context>(options => options.UseSqlServer(SecretConfiguration.connectionString));

            services.AddScoped<IProject2Repository, Project2Repository>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalAndAppServiceAngular", builder =>
                builder.WithOrigins("https://revdogsangular.azurewebsites.net" ,"http://revdogsangular.azurewebsites.net", "http://localhost:4200")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                );

            });

            services.AddControllers(options =>
            {
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
                options.ReturnHttpNotAcceptable = true;
                options.SuppressAsyncSuffixInActionNames = false;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RevDogs", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowLocalAndAppServiceAngular");

            app.UseAuthorization();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "RevDogs V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
