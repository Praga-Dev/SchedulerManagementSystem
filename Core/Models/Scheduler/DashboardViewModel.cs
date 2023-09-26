using SchedulerManagementSystem.Models;

namespace SchedulerManagementSystem.Models.Scheduler
{
    public class DashboardViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<EmployeeSchedulerVM> EmployeeSchedulerVMList { get; set; }
    }
}
