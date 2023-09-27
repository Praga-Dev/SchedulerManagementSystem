namespace SchedulerManagementSystem.DataModels.Scheduler
{
    public class EmployeeSchedulerInfoDB
    {
        public Guid Id { get; set; }
        public Guid EmployeeInfoId { get; set; }
        public DateTime WorkScheduledDate { get; set; }
        public int AvailableHours { get; set; }
        public int AllocatedHours { get; set; }
    }
}
