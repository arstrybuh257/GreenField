using Autofac;
using GreenField.DAL.DataAccess.Interfaces;
using GreenField.DAL.DataAccess.Mongo;
using GreenField.DAL.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace GreenField.Api.Extensions
{
    public static class MongoExtensions
    {
        public static void AddMongo(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var options = new MongoDbOptions();
                var configuration = context.Resolve<IConfiguration>();
                configuration.GetSection("mongo").Bind(options);
                return options;
            }).SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<MongoDbOptions>();

                return new MongoClient(options.ConnectionString);
            }).SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<MongoDbOptions>();
                var client = context.Resolve<MongoClient>();
                return client.GetDatabase(options.Database);

            }).InstancePerLifetimeScope();
        }

        public static void AddMongoRepository<TEntity>(this ContainerBuilder builder, string collectionName)
            where TEntity : BaseEntity
            => builder.Register(ctx => new MongoRepository<TEntity>(ctx.Resolve<IMongoDatabase>(), collectionName))
                .As<IRepository<TEntity>>()
                .InstancePerLifetimeScope();
    }
}