using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataAccess.Repositories;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataAccess.Repositories;

namespace SchedulerManagementSystem.IoC
{
    public static class DependencyRegistrations
    {
        public static void InjectDependencies(this IServiceCollection services, string connectionString)
        {
            // DB Conn - Install req. packages
            //services.AddTransient<IDataBaseConnection, DataBaseConnection>(con => new DataBaseConnection(connectionString));

            services.AddSingleton<ILocationRepository, LocationRepository>();
            services.AddSingleton<IGradeRepository, GradeRepository>();
            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<IEmployeeSchedulerRepository, EmployeeSchedulerRepository>();
        }
    }
}
