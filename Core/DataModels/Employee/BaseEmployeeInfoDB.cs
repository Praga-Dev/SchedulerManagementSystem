namespace SchedulerManagementSystem.DataModels.Employee
{
    public class BaseEmployeeInfoDB
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Guid LocationId { get; set; }
        public string LocationName { get; set; }
        public Guid GradeId { get; set; }
        public string GradeName { get; set; }
        public int TotalHours { get; set; }
    }
}
