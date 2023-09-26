using SchedulerManagementSystem.Models;

namespace SchedulerManagementSystem.Models.Scheduler
{
    public class EmployeeSchedulerVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GradeInfo GradeInfo { get; set; }
        public LocationInfo LocationInfo { get; set; }
        public int TotalHours { get; set; }

        // list of column (dates) in table (calendar)
        public List<EmployeeSchedulerInfo> EmployeeSchedulerInfoList { get; set; }
    }
}
