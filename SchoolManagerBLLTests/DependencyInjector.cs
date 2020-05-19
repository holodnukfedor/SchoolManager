using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagerDAL;
using SchoolManagerBLL;

namespace SchoolManagerBLLTests
{
    internal static class DependencyInjector
    {
        public static Lazy<IServiceProvider> ServiceProvider = new Lazy<IServiceProvider>(GetServiceProvider);

        private static IServiceProvider GetServiceProvider()
        {
            IConfigurationRoot config = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json")
                  .Build();
            string connectionStr = config.GetConnectionString("DefaultConnection");

            DatabaseDeployer databaseDeployer = new DatabaseDeployer(connectionStr, "Sql\\Schema.sql", "Sql\\Procedures.sql", "Sql\\Initialization.sql");
            databaseDeployer.DeployAsync().Wait();
            SchoolManagerDb schoolManagerDb = new SchoolManagerDb(connectionStr);
            StudentService studentService = new StudentService(schoolManagerDb);

            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<ISchoolManagerDb>(x => schoolManagerDb);
            services.AddSingleton<IStudentService>(x => studentService);

            return services.BuildServiceProvider();
        }
    }
}
