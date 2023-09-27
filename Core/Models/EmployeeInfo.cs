using SchedulerManagementSystem.Models.Lookups;

namespace SchedulerManagementSystem.Models
{
    public class EmployeeInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public GradeInfo GradeInfo { get; set; }
        public LocationInfo LocationInfo { get; set; }
        public int TotalHours { get; set; }
    }
}
