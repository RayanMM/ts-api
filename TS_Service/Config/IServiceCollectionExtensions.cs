using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using TS_Security;
using TS_Security.Config;

namespace TS_Api.Config
{
    public static partial class IServiceCollectionExtensions
    {
        public static IServiceCollection AddProtectedControllers(this IServiceCollection services)
        {
            services.AddControllers(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser()
                .Build();

                config.Filters.Add(new AuthorizeFilter(policy));
            });

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Description = "Authentication Bearer bia JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

                c.EnableAnnotations();

                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TS API", Version = "v1", Description = "Total Safe" });
                c.CustomSchemaIds(x => x.FullName);
            });

            return services;
        }

        public static IServiceCollection AddHealthCheck(this IServiceCollection services)
        {
            var config = services.BuildServiceProvider().GetService<IConfiguration>();

            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddSingleton<SigningConfigurations>();
            services.AddTransient<AuthenticationTokenProvider>();

            IConfiguration config = services.BuildServiceProvider().GetService<IConfiguration>();
            string secretKey = config["Auth:AUTH_SECRETKEY"];
            IConfigurationSection tokenConfigSection = config.GetSection("TokenConfigurations");

            TokenConfigurations tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(tokenConfigSection).Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidAudience = tokenConfigurations.Audience,
                    ValidIssuer = tokenConfigurations.Issuer,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }

        public static IServiceCollection AddProblemHandling(this IServiceCollection services)
        {
            var env = services.BuildServiceProvider().GetService<IWebHostEnvironment>();

            services.AddProblemDetails(config =>
            {
                config.IncludeExceptionDetails = _ => env.IsDevelopment();
            });

            return services;
        }
    }
}
