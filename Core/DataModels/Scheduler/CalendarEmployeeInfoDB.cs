using SchedulerManagementSystem.DataModels.Employee;

namespace SchedulerManagementSystem.DataModels.Scheduler
{
    public class CalendarEmployeeInfoDB : EmployeeInfoDB
    {
        public bool IsAddedToCalendar { get; set; }
    }
}