using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using GreenField.Api.Extensions;
using GreenField.Common.Configuration;
using GreenField.Common.Messaging;
using GreenField.Common.Messaging.Messages;
using GreenField.DAL.Entities;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace GreenField.Api
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
            services.AddControllers();
            services.ConfigureSwagger();
            services.ConfigureIoC();
            services.AddMediatR(typeof(Startup));
            services.AddSingleton<IRabbitMqBus, RabbitMqBus>();
            
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            
            services.ConfigureAutoMapper();
            services.ConfigureAuthentication(Configuration);
            
            services.Configure<JwtOptions>(Configuration.GetSection("JwtAuthentication"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GreenField.Api v1"));
            }
            //app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseRouting();

            RabbitMqBus bus = app.ApplicationServices.GetRequiredService<RabbitMqBus>();
            bus.Subscribe<PestDetectedMessage>("apiService");
            bus.Subscribe<WeedDetectedMessage>("apiService");
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseErrorHandler(true);
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();
            
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
            
            builder.Register<RabbitMqBus>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return new RabbitMqBus(c);
            });
            
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces();
            builder.AddMongo();
            builder.AddMongoRepository<Organisation>("Organisation");
            builder.AddMongoRepository<User>("Users");
            builder.AddMongoRepository<Drone>("Drones");
            builder.AddMongoRepository<Sensor>("Sensors");
            builder.AddMongoRepository<Weed>("Weeds");
            builder.AddMongoRepository<Pest>("Pests");
            builder.AddMongoRepository<Field>("Fields");
            builder.AddMongoRepository<Culture>("Culture");
            builder.AddMongoRepository<Pesticide>("Pesticides");
            builder.AddMongoRepository<Component>("Component");
        }
    }
}