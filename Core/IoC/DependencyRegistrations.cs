using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataAccess.Repositories;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using SchedulerManagementSystem.Common.CustomValidations.NoFutureDate;

namespace SchedulerManagementSystem.IoC
{
    public static class DependencyRegistrations
    {
        public static void InjectDependencies(this IServiceCollection services, string connectionString)
        {
            // DB Conn - Install req. packages
            //services.AddTransient<IDataBaseConnection, DataBaseConnection>(con => new DataBaseConnection(connectionString));

            services.AddSingleton<IEmployeeRepository, EmployeeRepository>();
            services.AddSingleton<IEmployeeSchedulerRepository, EmployeeSchedulerRepository>();
            
            // Adapter Providers
            //services.AddSingleton<IValidationAttributeAdapterProvider, NoFutureDateAdapterProvider>();

            // Lookups

            services.AddSingleton<ILocationRepository, LocationRepository>();
            services.AddSingleton<IGradeRepository, GradeRepository>();
            services.AddSingleton<IAccessLevelTypeRepository, AccessLevelTypeRepository>();
            services.AddSingleton<IATS_AdminLevelTypeRepository, ATS_AdminLevelTypeRepository>();
            services.AddSingleton<IATS_SecurityLevelTypeRepository, ATS_SecurityLevelTypeRepository>();
            services.AddSingleton<IBPA_ManagementLevelTypeRepository, BPA_ManagementLevelTypeRepository>();
            services.AddSingleton<IDepartmentRepository, DepartmentRepository>();
            services.AddSingleton<IEmployeeRoleRepository, EmployeeRoleRepository>();
            services.AddSingleton<IHireTypeRepository, HireTypeRepository>();
            services.AddSingleton<ITerminationTypeRepository, TerminationTypeRepository>();
            services.AddSingleton<ITitleRepository, TitleRepository>();
            
        }
    }
}
