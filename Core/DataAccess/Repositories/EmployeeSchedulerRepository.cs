using SchedulerManagementSystem.Common.Constants;
using SchedulerManagementSystem.Common.Models;
using SchedulerManagementSystem.Common.Utils;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels;
using SchedulerManagementSystem.DataAccess.IRepositories;
using SchedulerManagementSystem.DataModels.Scheduler;
using System.Runtime.Intrinsics;

namespace SchedulerManagementSystem.DataAccess.Repositories
{
    public class EmployeeSchedulerRepository : IEmployeeSchedulerRepository
    {
        public static List<EmployeeSchedulerInfoDB> EmployeeSchedulerInfoList = new();

        private readonly IEmployeeRepository _employeeRepository;
        private readonly List<EmployeeSchedulerInfoDB> _EmployeeSchedulerInfoList = EmployeeSchedulerInfoList;


        // TODO make as CalendarEmployeeInfoDB model instead of GUID
        public static List<Guid> CalendarEmployeeInfoList = new()
        {
            new Guid("4A053397-E1FC-43F8-815C-93ACC81EB94F"),
            new Guid("83E669D6-EDC3-4320-9C25-E8B3EB592763"),
            new Guid("6567591C-E61D-4FB4-A8D7-9E6FBA68DE5E"),
            new Guid("91113354-54A0-4B1D-8314-F94FC838489E")
        };

        public EmployeeSchedulerRepository(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<Response<Guid>> UpdateAllocationWorkingHour(Guid employeeSchedulerId, int allocatedWorkingHour, Guid loggedInUserId)
        {
            Response<Guid> response = new();

            try
            {
                var targetData = _EmployeeSchedulerInfoList.Where(empSch => empSch.Id == employeeSchedulerId).FirstOrDefault();
                if (targetData != null)
                {
                    if (targetData.WorkScheduledDate < DateTime.Now.Date)
                    {
                        response.Message = ResponseConstants.ERROR_PAST_DATE_CANNOT_BE_CHANGED;
                        return response;

                    }
                    var oldAllocatedHour = targetData.AllocatedHours;
                    targetData.AllocatedHours = allocatedWorkingHour;

                    // Update totalHours of employee
                    var emp = await _employeeRepository.GetEmployeeById(targetData.EmployeeInfoId, loggedInUserId);
                    if (emp != null)
                    {
                        var updatedHourForEmployee = allocatedWorkingHour - oldAllocatedHour;
                        emp.Data.TotalHours += updatedHourForEmployee;
                    }

                    response.Data = employeeSchedulerId;
                    response.IsSuccess = true;
                    response.Message = ResponseConstants.SUCCESS;
                    return response;
                }

                response.Message = ResponseConstants.NO_RECORDS_FOUND;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
            }

            return response;
        }

        public async Task<Response<List<EmployeeSchedulerInfoDB>>> GetEmployeeSchedulerCalendar(DateTime startDate, DateTime endDate, Guid loggedInUserId)
        {
            Response<List<EmployeeSchedulerInfoDB>> response = new() { Data = new() };

            try
            {
                //Get All Employees List

                List<EmployeeInfoDB> employeeList = new();

                var employeeListResponse = await _employeeRepository.GetEmployees(loggedInUserId);
                if (Helpers.IsValidResponse(employeeListResponse))
                {
                    employeeList = employeeListResponse.Data.Where(
                        emp => CalendarEmployeeInfoList.Contains(emp.Id)).ToList();
                }



                // Generate All Missing date's data by using default values
                if (employeeList.Any())
                {
                    for (DateTime currDate = startDate.Date; currDate.Date <= endDate.Date; currDate = currDate.AddDays(1))
                    {
                        if (currDate.DayOfWeek == DayOfWeek.Saturday || currDate.DayOfWeek == DayOfWeek.Sunday)
                        {
                            continue;
                        }

                        foreach (var emp in employeeList)
                        {
                            var schedulerRecordData = _EmployeeSchedulerInfoList.Where(x => x.EmployeeInfoId == emp.Id && x.WorkScheduledDate.Date == currDate.Date).FirstOrDefault();

                            // Add data if already available else create dummy data
                            if (schedulerRecordData != null && Helpers.IsValidGuid(schedulerRecordData.Id))
                            {
                                response.Data.Add(_EmployeeSchedulerInfoList.Where(x => x.Id == schedulerRecordData.Id).First());
                            }
                            else
                            {
                                // dummy data

                                var dummy = new EmployeeSchedulerInfoDB()
                                {
                                    Id = Guid.NewGuid(),
                                    AllocatedHours = 0,
                                    AvailableHours = 10,
                                    WorkScheduledDate = currDate,
                                    EmployeeInfoId = emp.Id
                                };

                                EmployeeSchedulerInfoList.Add(dummy);
                                response.Data.Add(dummy);
                            }
                        }
                    }

                    response.IsSuccess = true;
                    response.Message = ResponseConstants.SUCCESS;
                    return response;
                }

                response.Message = ResponseConstants.FAILED;
            }
            catch (Exception ex)
            {
                // log error
                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
            }

            return response;
        }

        public async Task<Response<List<CalendarEmployeeInfoDB>>> GetManageCalendarEmployeeList(Guid loggedInUserId)
        {
            Response<List<CalendarEmployeeInfoDB>> response = new();

            try
            {
                var employeeListResponse = await _employeeRepository.GetEmployees(loggedInUserId);
                if (Helpers.IsValidResponse(employeeListResponse))
                {
                    //Mapper
                    List<CalendarEmployeeInfoDB> calendarEmployeeInfoDBList = new();

                    foreach (var employee in employeeListResponse.Data)
                    {
                        calendarEmployeeInfoDBList.Add(
                            new()
                            {
                                Id = employee.Id,
                                Name = employee.Name,
                                GradeId = employee.GradeId,
                                GradeName = employee.GradeName,
                                LocationId = employee.LocationId,
                                LocationName = employee.LocationName,
                                TotalHours = employee.TotalHours,
                                IsAddedToCalendar = CalendarEmployeeInfoList.Contains(employee.Id),
                            });
                    }

                    response.Data = calendarEmployeeInfoDBList;
                    response.IsSuccess = true;
                    response.Message = ResponseConstants.SUCCESS;
                    return response;
                }

                response.Message = ResponseConstants.FAILED;
            }
            catch (Exception ex)
            {
                // log error
                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
            }

            return response;
        }

        public async Task<Response<Guid>> AddEmployeeFromCalendar(Guid employeeId, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            try
            {
                if (Helpers.IsValidGuid(employeeId))
                {
                    var employeeInfoListResponse = await _employeeRepository.GetEmployees(loggedInUserId);

                    if (Helpers.IsValidResponse(employeeInfoListResponse))
                    {
                        var targetEmployee = employeeInfoListResponse.Data.Where(emp => emp.Id == employeeId).FirstOrDefault();
                        if (targetEmployee != null)
                        {
                            CalendarEmployeeInfoList.Add(employeeId);
                            response.Data = employeeId;
                            response.IsSuccess = true;
                            response.Message = ResponseConstants.SUCCESS;
                            return response;
                        }
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

        public async Task<Response<Guid>> RemoveEmployeeFromCalendar(Guid employeeId, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            try
            {
                if (Helpers.IsValidGuid(employeeId))
                {
                    var employeeInfoListResponse = await _employeeRepository.GetEmployees(loggedInUserId);

                    if (Helpers.IsValidResponse(employeeInfoListResponse))
                    {
                        var targetEmployee = employeeInfoListResponse.Data.Where(emp => emp.Id == employeeId).FirstOrDefault();
                        if (targetEmployee != null)
                        {
                            response.IsSuccess = CalendarEmployeeInfoList.Remove(employeeId);
                            if (response.IsSuccess)
                            {
                                response.Data = employeeId;
                                response.Message = ResponseConstants.SUCCESS;
                                return response;
                            }
                        }
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

        public async Task<Response<bool>> AddBulkEmployeesFromCalendar(List<Guid> employeeIdList, Guid loggedInUserId)
        {
            Response<bool> response = new();
            try
            {
                if (employeeIdList.Any())
                {

                    foreach (var employeeId in employeeIdList)
                    {
                        CalendarEmployeeInfoList.Add(employeeId);
                    }

                    CalendarEmployeeInfoList = CalendarEmployeeInfoList.Distinct().ToList();

                    response.IsSuccess = true;
                    response.Data = true;
                    response.Message = ResponseConstants.SUCCESS;
                    return response;
                }

                response.Message = ResponseConstants.FAILED;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
            }

            return response;
        }

        public async Task<Response<bool>> RemoveBulkEmployeesFromCalendar(List<Guid> employeeIdList, Guid loggedInUserId)
        {
            Response<bool> response = new();
            try
            {
                if (employeeIdList.Any())
                {

                    foreach (var employeeId in employeeIdList)
                    {
                        CalendarEmployeeInfoList.Remove(employeeId);
                    }

                    response.IsSuccess = true;
                    response.Data = true;
                    response.Message = ResponseConstants.SUCCESS;
                    return response;
                }

                response.Message = ResponseConstants.FAILED;
            }
            catch (Exception ex)
            {
                response.Message = ResponseConstants.INTERNAL_SERVER_ERROR;
            }

            return response;
        }

        public async Task<Response<Guid>> ReplaceEmployeeFromCalendar(Guid oldEmployeeId, Guid newEmployeeId, Guid loggedInUserId)
        {
            Response<Guid> response = new();
            try
            {
                if (Helpers.IsValidGuid(oldEmployeeId) && Helpers.IsValidGuid(newEmployeeId))
                {
                    var employeeInfoListResponse = await _employeeRepository.GetEmployees(loggedInUserId);

                    if (Helpers.IsValidResponse(employeeInfoListResponse))
                    {
                        var oldEmployee = employeeInfoListResponse.Data.Where(emp => emp.Id == oldEmployeeId).FirstOrDefault();
                        var newEmployee = employeeInfoListResponse.Data.Where(emp => emp.Id == newEmployeeId).FirstOrDefault();
                        if (oldEmployee != null && newEmployee != null)
                        {
                            // Replace employee
                            CalendarEmployeeInfoList.Remove(oldEmployeeId);
                            CalendarEmployeeInfoList.Add(newEmployeeId);

                            // Update Total Hours
                            newEmployee.TotalHours = oldEmployee.TotalHours;
                            oldEmployee.TotalHours = 0;

                            // Update all data from old employee to new employee
                            var target = EmployeeSchedulerInfoList.Where(emp => emp.EmployeeInfoId == oldEmployeeId).ToList();
                            if (target != null && target.Any())
                            {
                                target.ForEach(emp => emp.EmployeeInfoId = newEmployeeId);
                            }

                            response.Data = newEmployeeId;
                            response.IsSuccess = true;
                            response.Message = ResponseConstants.SUCCESS;
                            return response;
                        }

                        // Todo add valid reponse messages
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
    }
}
