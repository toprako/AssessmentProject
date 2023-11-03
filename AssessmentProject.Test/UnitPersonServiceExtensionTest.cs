using AssessmentProject.Persons.Extensions;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentProject.Test
{
    public class UnitPersonServiceExtensionTest
    {

        [Fact]
        public static void ConfigureCors_Test()
        {
            var service = A.Fake<IServiceCollection>();
            ServiceExtensions.ConfigureCors(service);
        }

        [Fact]
        public static void ConfigurePostgreSqlContext_Test()
        {
            var service = A.Fake<IServiceCollection>();
            var config = A.Fake<IConfiguration>();
            ServiceExtensions.ConfigurePostgreSqlContext(service, config);
        }

        [Fact]
        public static void ConfigureRepositoryWrapper_Test()
        {
            var service = A.Fake<IServiceCollection>();
            ServiceExtensions.ConfigureRepositoryWrapper(service);
        }

        [Fact]
        public static void ConfigureRabbitMq_Test()
        {
            var service = A.Fake<IServiceCollection>();
            var config = A.Fake<IConfiguration>();
            ServiceExtensions.ConfigureRabbitMq(service, config);
        }
    }
}
