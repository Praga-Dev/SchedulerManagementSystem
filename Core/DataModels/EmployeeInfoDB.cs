namespace SchedulerManagementSystem.DataModels
{
    public class EmployeeInfoDB
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid GradeId { get; set; }
        public string GradeName { get; set; }
        public Guid LocationId { get; set; }
        public string LocationName { get; set; }
        public int TotalHours { get; set; }
    }
}
