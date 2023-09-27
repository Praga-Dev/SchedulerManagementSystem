using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels.Employee;

namespace SchedulerManagementSystem.DataAccess.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly ILocationRepository _locationRepository;

        private static List<EmployeeInfoDB> _EmployeeInfoList = new()
            {
                new() {Id = new("4A053397-E1FC-43F8-815C-93ACC81EB94F"), Name = "Alex, Farrell", GradeId = new("2F880FC2-87A4-41FA-A1C9-7481B4ED0E68"), LocationId = new("455765AA-03A5-40C0-9997-242470DFA9AE"),TotalHours = 0 },
                new() {Id = new("83E669D6-EDC3-4320-9C25-E8B3EB592763"), Name = "Brian, Webster", GradeId = new("404A7104-F67C-4448-9EE5-DB1359E37A35"), LocationId = new("FC438154-45A3-4FD2-A150-354803DB8B2C"),TotalHours = 0 },
                new() {Id = new("6567591C-E61D-4FB4-A8D7-9E6FBA68DE5E"), Name = "Catherine, Emerson", GradeId = new("D5DAF4F9-1CF9-477B-8E2F-F668F98EC49A"), LocationId = new("9CED8A68-EEC6-467D-8625-8FA54379DE26"),TotalHours = 0 },
                new() {Id = new("91113354-54A0-4B1D-8314-F94FC838489E"), Name = "Diana, Newport", GradeId = new("435EF0D5-D5CC-4CD6-969A-419200678422"), LocationId = new("0477fd73-289b-4ef0-a175-29e5691b26c7"),TotalHours = 0 },
                new() {Id = new("FF7D999C-6C7F-480E-8EE3-E7B581616F8F"), Name = "Evans,Tatlow", GradeId = new("435EF0D5-D5CC-4CD6-969A-419200678422"), LocationId = new("0477fd73-289b-4ef0-a175-29e5691b26c7"),TotalHours = 0 },
                new() {Id = new("10E775DB-01BA-47BD-B7BF-9949E3AB9FCB"), Name = "Festos, Ashdown", GradeId = new("435EF0D5-D5CC-4CD6-969A-419200678422"), LocationId = new("0477fd73-289b-4ef0-a175-29e5691b26c7"),TotalHours = 0 },
                new() {Id = new("E4E151A4-0C6D-4C99-AD16-CB8769721FC1"), Name = "George, Morgan", GradeId = new("435EF0D5-D5CC-4CD6-969A-419200678422"), LocationId = new("0477fd73-289b-4ef0-a175-29e5691b26c7"),TotalHours = 0 },
                new() {Id = new("B8DB43B1-FCD9-42AF-970E-D77189CD33EE"), Name = "Harry, Newbry", GradeId = new("435EF0D5-D5CC-4CD6-969A-419200678422"), LocationId = new("0477fd73-289b-4ef0-a175-29e5691b26c7"),TotalHours = 0 },
                new() {Id = new("ECA8A67A-B14C-405D-BB22-57EB10FF2535"), Name = "Ivy Green, Desborough", GradeId = new("435EF0D5-D5CC-4CD6-969A-419200678422"), LocationId = new("0477fd73-289b-4ef0-a175-29e5691b26c7"),TotalHours = 0 },
                new() {Id = new("B9E139EA-55EA-4B56-A19B-847E14609629"), Name = "Josephine, Harding", GradeId = new("435EF0D5-D5CC-4CD6-969A-419200678422"), LocationId = new("0477fd73-289b-4ef0-a175-29e5691b26c7"),TotalHours = 0 },
                new() {Id = new("CFBCCDC9-19B4-4445-80EB-3029E735935D"), Name = "Kili, Sutton", GradeId = new("435EF0D5-D5CC-4CD6-969A-419200678422"), LocationId = new("0477fd73-289b-4ef0-a175-29e5691b26c7"),TotalHours = 0 },
                new() {Id = new("1EDFEC2C-3C13-4072-A6BE-F12C60C18D6F"), Name = "Leon, Rogan", GradeId = new("435EF0D5-D5CC-4CD6-969A-419200678422"), LocationId = new("0477fd73-289b-4ef0-a175-29e5691b26c7"),TotalHours = 0 },
                new() {Id = new("9D4F024C-83A1-4FDC-B711-1D6EE18C3E61"), Name = "Marry, Nash", GradeId = new("435EF0D5-D5CC-4CD6-969A-419200678422"), LocationId = new("0477fd73-289b-4ef0-a175-29e5691b26c7"),TotalHours = 0 },
                new() {Id = new("29E2AFD6-56BE-4B7C-AC97-BEE90B113730"), Name = "Neo, Potter", GradeId = new("435EF0D5-D5CC-4CD6-969A-419200678422"), LocationId = new("0477fd73-289b-4ef0-a175-29e5691b26c7"),TotalHours = 0 },
                new() {Id = new("48D9FF76-9ADB-4EEF-A3D0-CE60FA95CCDF"), Name = "Olivia, Pendrick", GradeId = new("435EF0D5-D5CC-4CD6-969A-419200678422"), LocationId = new("0477fd73-289b-4ef0-a175-29e5691b26c7"),TotalHours = 0 },
            };

        //public readonly List<EmployeeInfoDB> EmployeeInfoList = _EmployeeInfoList;

        public EmployeeRepository(IGradeRepository gradeRepository, ILocationRepository locationRepository)
        {
            _gradeRepository = gradeRepository;
            _locationRepository = locationRepository;
        }

        public async Task<Response<Guid>> CreateEmployee(EmployeeInfoDB employeeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.NewGuid();

            try
            {
                if (employeeInfoDB != null && Helpers.IsValidGuid(loggedInUserId))
                {
                    employeeInfoDB.Id = id;
                    employeeInfoDB.TotalHours = 0;
                    _EmployeeInfoList.Add(employeeInfoDB);

                    response.Data = id;
                    response.IsSuccess = true;
                    response.Message = ResponseConstants.SUCCESS;
                    return response;
                }

                response.Message = ResponseConstants.FAILED;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<Guid>> UpdateEmployee(EmployeeInfoDB employeeInfoDB, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            Guid id = Guid.Empty;

            try
            {
                if (employeeInfoDB != null && Helpers.IsValidGuid(employeeInfoDB.Id))
                {
                    EmployeeInfoDB? employee = _EmployeeInfoList.FirstOrDefault(gr => gr.Id == employeeInfoDB.Id);
                    if (employee != null)
                    {
                        employee.Name = employeeInfoDB.Name;

                        // CHECK ON DB todo check the value is valid or not
                        employee.LocationId = employeeInfoDB.LocationId;

                        // CHECK ON DB todo check the value is valid or not
                        employee.GradeId = employeeInfoDB.GradeId;

                        response.Data = employeeInfoDB.Id;
                        response.IsSuccess = true;
                        response.Message = ResponseConstants.SUCCESS;
                        return response;
                    }

                    response.Message = ResponseConstants.FAILED;
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<List<EmployeeInfoDB>>> GetEmployees(Guid loggedInUserId)
        {
            var data = new List<EmployeeInfoDB>();

            foreach (var employee in _EmployeeInfoList) 
            {
                var emp = employee;

                var locResponse = await _locationRepository.GetLocationById(emp.LocationId, loggedInUserId);
                if (Helpers.IsValidResponse(locResponse) && !string.IsNullOrWhiteSpace(locResponse.Data.Name))
                {
                    emp.LocationName = locResponse.Data.Name;
                }

                var gradResponse = await _gradeRepository.GetGradeById(emp.GradeId, loggedInUserId);
                if (Helpers.IsValidResponse(gradResponse) && !string.IsNullOrWhiteSpace(gradResponse.Data.Name))
                {
                    emp.GradeName = gradResponse.Data.Name;
                }

                data.Add(emp);
            }

            return new()
            {
                Data = data,
                IsSuccess = true,
                Message = ResponseConstants.SUCCESS
            };
        }

        public async Task<Response<EmployeeInfoDB>> GetEmployeeById(Guid employeeId, Guid loggedInUserId)
        {
            Response<EmployeeInfoDB> response = new();

            try
            {
                if (Helpers.IsValidGuid(employeeId))
                {

                    var emp = _EmployeeInfoList.FirstOrDefault(emp => emp.Id == employeeId);

                    if (emp != null) 
                    {
                        var locResponse = await _locationRepository.GetLocationById(emp.LocationId, loggedInUserId);
                        if (Helpers.IsValidResponse(locResponse) && !string.IsNullOrWhiteSpace(locResponse.Data.Name))
                        {
                            emp.LocationName = locResponse.Data.Name;
                        }

                        var gradResponse = await _locationRepository.GetLocationById(emp.LocationId, loggedInUserId);
                        if (Helpers.IsValidResponse(gradResponse) && !string.IsNullOrWhiteSpace(gradResponse.Data.Name))
                        {
                            emp.GradeName = gradResponse.Data.Name;
                        }
                        
                        return new()
                        {
                            IsSuccess = true,
                            Message = ResponseConstants.SUCCESS,
                            Data = emp
                        };
                    }

                }

                response.Message = ResponseConstants.FAILED;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
            }

            return response;
        }

        public async Task<Response<Guid>> DeleteEmployeeById(Guid employeeId, Guid loggedInUserId)
        {
            Response<Guid> response = new();

            try
            {
                if (Helpers.IsValidGuid(employeeId))
                {
                    _EmployeeInfoList = _EmployeeInfoList.Where(gr => gr.Id != employeeId).ToList();
                    response.Data = employeeId;
                    response.IsSuccess = true;
                    response.Message = ResponseConstants.SUCCESS;
                    return response;
                }

                response.Message = ResponseConstants.FAILED;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<Guid>> UpdateEmployeeTotalWorkingHours(Guid employeeId, int totalWorkingHours, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            response.Data = Guid.Empty;

            try
            {
                if (Helpers.IsValidGuid(employeeId))
                {
                    EmployeeInfoDB? employee = _EmployeeInfoList.FirstOrDefault(emp => emp.Id == employeeId);
                    if (employee != null)
                    {
                        employee.TotalHours = totalWorkingHours;

                        response.Data = employeeId;
                        response.IsSuccess = true;
                        response.Message = ResponseConstants.SUCCESS;
                        return response;
                    }

                    response.Message = ResponseConstants.FAILED;
                }
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.SOMETHING_WENT_WRONG;
            }

            return response;
        }

        public async Task<Response<List<Guid>>> GetEmployeesId(Guid loggedInUserId)
        {
            return new()
            {
                IsSuccess = true,
                Message = ResponseConstants.SUCCESS,
                Data = _EmployeeInfoList.Select(emp => emp.Id).ToList()
            };
        }
    }
}
