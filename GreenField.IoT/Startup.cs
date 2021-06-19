using System.Reflection;
using Autofac;
using GreenField.Common.Messaging;
using GreenField.Common.Messaging.Messages;
using GreenField.IoT.Utils;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace GreenField.IoT
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "GreenField.IoT", Version = "v1"});
            });
            services.AddMediatR(typeof(Startup));
            services.AddSingleton<IRabbitMqBus, RabbitMqBus>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GreenField.IoT v1"));
            }

            RabbitMqBus bus = app.ApplicationServices.GetRequiredService<RabbitMqBus>();
            bus.Subscribe<CheckFieldOnDemand>("iotservice");
            
            app.ApplicationServices.GetRequiredService<BackgroundCheckWorker>().DoWork();
            
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
        
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();
            
            builder
                .RegisterType<BackgroundCheckWorker>()
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
            }).InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .AsImplementedInterfaces();
        }
    }
}