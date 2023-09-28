using SchedulerManagementSystem.Models.Employee;

namespace SchedulerManagementSystem.Models.Scheduler
{
    public class EmployeeSchedulerVM : BaseEmployeeInfo
    {
        // list of column (dates) in table (calendar)
        public List<EmployeeSchedulerInfo> EmployeeSchedulerInfoList { get; set; }
    }
}
