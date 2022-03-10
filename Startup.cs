using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using RegistrationLoginApi.Authorization;
using RegistrationLoginApi.Helpers;
using RegistrationLoginApi.Services;
using RegistrationLoginApi.Data;
using RegistrationLoginApi.Data.Mapping;

namespace WebApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        // add services to the DI container
        public void ConfigureServices(IServiceCollection services)
        {
            // use sql server db in production and sqlite db in development
            services.AddAutoMapper(typeof(MappingProfile));

            string connectionString = _configuration.GetConnectionString("default");
            services.AddDbContext<AppDbContext>(c=>c.UseNpgsql(connectionString));
            services.AddScoped<IAppDbContext, AppDbContext>();
            services.AddScoped<IRepository, Repository>();
            
            services.AddCors();
            services.AddControllers();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // configure strongly typed settings objects
            services.Configure<AppSettings>(_configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();
        }

        // configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // migrate any database changes on startup (includes initial db creation)
            //dataContext.Database.Migrate();

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
