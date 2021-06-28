using FluentMigrator.Runner;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TS.Domain.Helper;
using TS.IoC;
using TS_Api.Config;

namespace TS_Service
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
            services.AddProtectedControllers();
            services.AddServices();
            services.AddSecurity();
            services.AddDatabase();
            services.AddRepositories();
            services.AddCors();
            services.AddHealthCheck();
            services.AddSwagger();
            services.AddAuth();
            services.AddProblemHandling();
            services.AddMigrator();

            services.AddTransient<CacheHelper>();

            string redisConnection = Configuration.GetValue<string>("Auth:REDIS_CONNECTION");

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = redisConnection;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IMigrationRunner migrationRunner)
        {
            app.UseProblemDetails();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCorsConfig();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpointsConfig();
            app.UseSwaggerConfig();
            app.UseStaticFiles();

			#region migrator

            migrationRunner.MigrateUp();

			#endregion
		}
	}
}
