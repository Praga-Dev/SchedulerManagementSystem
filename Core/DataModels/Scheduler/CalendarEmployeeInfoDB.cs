using SchedulerManagementSystem.DataModels.Employee;

namespace SchedulerManagementSystem.DataModels.Scheduler
{
    public class CalendarEmployeeInfoDB : BaseEmployeeInfoDB
    {
        public bool IsAddedToCalendar { get; set; }
    }
}