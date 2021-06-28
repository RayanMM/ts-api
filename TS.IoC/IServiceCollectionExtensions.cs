using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data.SqlClient;
using System.Reflection;
using TS.Infra.Context;
using TS.Infra.Base;
using TS.Service.Base;
using TS_Security;
using FluentMigrator.Runner;
using TS.Infra.Migrations;

namespace TS.IoC
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
               .FromAssemblyDependencies(Assembly.GetExecutingAssembly())
               .AddClasses(classes => classes.AssignableTo<IService>())
               .AsSelfWithInterfaces()
               .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddSecurity(this IServiceCollection services)
        {
            services.Scan(scan => scan
               .FromAssemblyDependencies(Assembly.GetExecutingAssembly())
               .AddClasses(classes => classes.AssignableTo<ISecurity>())
               .AsSelfWithInterfaces()
               .WithScopedLifetime());

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            IConfiguration config = services.BuildServiceProvider().GetService<IConfiguration>();
            services.AddScoped(x => new DbContext(new SqlConnection(config["auth:CONNECTION_TS"])));
            services.AddScoped<UnitOfWork>();
            return services;

        }

        public static IServiceCollection AddMigrator(this IServiceCollection services)
        {
            IConfiguration config = services.BuildServiceProvider().GetService<IConfiguration>();

            services.AddFluentMigratorCore()
            .ConfigureRunner(
                builder => builder
                       .AddSqlServer()
                       .WithGlobalConnectionString(config["auth:CONNECTION_TS"])
                       .ScanIn(typeof(Migration202003041045).Assembly).For.Migrations()) ;

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.Scan(scan => scan
               .FromAssemblyDependencies(Assembly.GetExecutingAssembly())
               .AddClasses(classes => classes.AssignableTo<IRepository>())
               .AsSelf()
               .WithScopedLifetime());

            return services;
        }
    }
}
