using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.DataModels;

namespace SchedulerManagementSystem.DataAccess.IRepositories
{
    public interface IEmployeeRepository
    {
        Task<Response<Guid>> CreateEmployee(EmployeeInfoDB employeeInfoDb, Guid loggedInUserId);
        Task<Response<Guid>> UpdateEmployee(EmployeeInfoDB employeeInfoDb, Guid loggedInUserId);
        Task<Response<Guid>> UpdateEmployeeTotalWorkingHours(Guid employeeId, int totalWorkingHours, Guid loggedInUserId);
        Task<Response<List<EmployeeInfoDB>>> GetEmployees(Guid loggedInUserId);
        Task<Response<List<Guid>>> GetEmployeesId(Guid loggedInUserId);
        Task<Response<EmployeeInfoDB>> GetEmployeeById(Guid employeeId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteEmployeeById(Guid employeeId, Guid loggedInUserId);
    }
}
