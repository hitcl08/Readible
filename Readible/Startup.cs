using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Readible.Domain.Interfaces;
using Readible.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Readible.Domain.Repositories.EntityFramework;
using AutoMapper;
using Readible.Domain.Repositories.EntityFramework.ViewModels;
using Readible.Domain.Models;

namespace Readible
{
    public class Startup
    {
        private readonly string _connectionString;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _connectionString = "Server=tcp:readible.database.windows.net,1433;Initial Catalog=readible-db;Persist Security Info=False;User ID=liamhitchcock;Password=4fBuJ976YAUdwRXQ;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;";

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // TODO APP INSIGHTS
            //services.ConfigureApplicationInsights(Configuration);

            services.AddDbContext<ReadibleContext>(options => options.UseSqlServer(_connectionString)
            .EnableSensitiveDataLogging());

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();
            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            
            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Readible", Version = "v1" });
            });
            services.AddRouting(opt => opt.LowercaseUrls = true);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Readible v1"));
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
