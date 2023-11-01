using AssessmentProject.Report.Consumers;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace AssessmentProject.Report.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
        }
        public static void ConfigurePostgreSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["PostgreSqlConnection:ConnectionString"];
            services.AddDbContext<RepositoryContext>(con => con.UseNpgsql(connectionString, b => b.MigrationsAssembly("AssessmentProject.Report")));
        }
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureRabbitMq(this IServiceCollection services, IConfiguration config)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumersFromNamespaceContaining<UpdateReportDataConsumer>();
                x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("reportLastStatus", false));

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(config["RabbitMq:Host"], "/", host =>
                    {
                        host.Username(config.GetValue("RabbitMq:Username", "guest"));
                        host.Password(config.GetValue("RabbitMq:Password", "guest"));
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
