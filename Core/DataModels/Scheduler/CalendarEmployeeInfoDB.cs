using SchedulerManagementSystem.DataModels;

namespace SchedulerManagementSystem.DataModels.Scheduler
{
    public class CalendarEmployeeInfoDB : EmployeeInfoDB
    {
        public bool IsAddedToCalendar { get; set; }
    }
}