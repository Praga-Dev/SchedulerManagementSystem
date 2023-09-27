using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.DataModels;
using SchedulerManagementSystem.DataModels.Scheduler;

namespace SchedulerManagementSystem.DataAccess.IRepositories
{
    public interface IEmployeeSchedulerRepository
    {
        Task<Response<Guid>> UpdateAllocationWorkingHour(Guid employeeSchedulerId, int allocatedWorkingHour, Guid loggedInUserId);
        Task<Response<List<EmployeeSchedulerInfoDB>>> GetEmployeeSchedulerCalendar(DateTime startDate, DateTime endDate, Guid loggedInUserId);
        Task<Response<List<CalendarEmployeeInfoDB>>> GetManageCalendarEmployeeList(Guid loggedInUserId);
        Task<Response<Guid>> AddEmployeeFromCalendar(Guid employeeId, Guid loggedInUserId);
        Task<Response<Guid>> ReplaceEmployeeFromCalendar(Guid oldEmployeeId, Guid newEmployeeId, Guid loggedInUserId);
        Task<Response<Guid>> RemoveEmployeeFromCalendar(Guid employeeId, Guid loggedInUserId);
        Task<Response<bool>> AddBulkEmployeesFromCalendar(List<Guid> employeeIdList, Guid loggedInUserId);
        Task<Response<bool>> RemoveBulkEmployeesFromCalendar(List<Guid> employeeIdList, Guid loggedInUserId);
    }
}
