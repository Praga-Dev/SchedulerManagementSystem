using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.DataModels.Lookups;

namespace SchedulerManagementSystem.DataAccess.IRepositories
{
    public interface IDepartmentRepository
    {
        Task<Response<Guid>> CreateDepartment(DepartmentInfoDB departmentInfoDb, Guid loggedInUserId);
        Task<Response<Guid>> UpdateDepartment(DepartmentInfoDB departmentInfoDb, Guid loggedInUserId);
        Task<Response<List<DepartmentInfoDB>>> GetDepartments(Guid loggedInUserId);
        Task<Response<DepartmentInfoDB>> GetDepartmentById(Guid departmentId, Guid loggedInUserId);
        Task<Response<Guid>> DeleteDepartmentById(Guid departmentId, Guid loggedInUserId);
    }
}
