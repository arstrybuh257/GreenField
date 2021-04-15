using System.Collections.Generic;
using System.Text;
using AutoMapper.Configuration;
using GreenField.Api.AutoMapper;
using GreenField.BLL.Services;
using GreenField.BLL.Services.Interfaces;
using GreenField.DAL.Repositories;
using GreenField.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace GreenField.Api.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection ConfigureIoC(this IServiceCollection services)
        {
            services.AddScoped<IOrganisationService, OrganisationService>();
            services.AddScoped<IOrganisationRepository, OrganisationRepository>();
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            
            services.AddScoped<IDroneRepository, DroneRepository>();
            services.AddScoped<IDroneService, DroneService>();
            
            services.AddScoped<ISensorService, SensorService>();
            services.AddScoped<ISensorRepository, SensorRepository>();
            
            services.AddScoped<IWeedService, WeedService>();
            services.AddScoped<IWeedRepository, WeedRepository>();
            
            services.AddScoped<IPestService, PestService>();
            services.AddScoped<IPestRepository, PestRepository>();
            
            services.AddScoped<IPesticideService, PesticideService>();
            services.AddScoped<IPesticideRepository, PesticideRepository>();
            
            services.AddScoped<ICultureService, CultureService>();
            services.AddScoped<ICultureRepository, CultureRepository>();
            
            services.AddScoped<IFieldService, FieldService>();
            services.AddScoped<IFieldRepository, FieldRepository>();
            
            services.AddScoped<IBackupService, BackupService>();
            return services;
        }
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperConfiguration));
        }

        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("MySecretSecretFlyAway"))
                };
            });

            return services;
        }


        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FlyAway", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };
                var requirements = new OpenApiSecurityRequirement
                {
                    {securityScheme, new List<string>()}
                };
                c.AddSecurityRequirement(requirements);
            });

            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            return services;
        }
    }
}