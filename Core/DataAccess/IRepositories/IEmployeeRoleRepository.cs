using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.IRepositories
{
    public interface IEmployeeRoleRepository
    {
        Task<Response<Guid>> CreateEmployeeRole(EmployeeRoleInfoDB employeeRoleInfoDb, Guid loggedInUserId);
        Task<Response<Guid>> UpdateEmployeeRole(EmployeeRoleInfoDB employeeRoleInfoDb, Guid loggedInUserId);
        Task<Response<List<EmployeeRoleInfoDB>>> GetEmployeeRoles(Guid loggedInUserId);
        Task<Response<EmployeeRoleInfoDB>> GetEmployeeRoleById(Guid employeeRoleId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteEmployeeRoleById(Guid employeeRoleId, Guid loggedInUserId);
    }
}
