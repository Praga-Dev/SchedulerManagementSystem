$(document).ready(function () {
    initDataTable();
});

function getPrevious3MonthsOnCalendar() {
    let curr = new Date($('#calendarSwitchBtnGroup').data('start-date'));
    if (curr && isValidDate(curr)) {
        loadSpinner();
        let currStartdate = curr.toLocaleDateString();
        $.ajax({
            url: `./employee-scheduler/previous-quarter/`,
            data: {
                "endDateStr": currStartdate
            },
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#employeeSchedulerCalendarContainer').empty().html(response);
                    initDataTable();
                }
                else {
                    showErrorMsg('Something went wrong !');
                }
            },
            error: function (err) {
                showErrorMsg('Something went wrong !');
            },
            complete: function () {
                hideSpinner();
            }
        })
    } else {
        showErrorMsg('Something went wrong !');
    }
}

function getNext3MonthsOnCalendar() {
    let curr = new Date($('#calendarSwitchBtnGroup').data('end-date'));
    if (curr && isValidDate(curr)) {
        loadSpinner();
        let currStartdate = curr.toLocaleDateString();
        $.ajax({
            url: `./employee-scheduler/next-quarter/`,
            data: {
                "startDateStr": currStartdate
            },
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#employeeSchedulerCalendarContainer').empty().html(response);
                    initDataTable();
                }
                else {
                    showErrorMsg('Something went wrong !');
                }
            },
            error: function (err) {
                showErrorMsg('Something went wrong !');
            },
            complete: function () {
                hideSpinner();
            }
        })
    } else {
        showErrorMsg('Something went wrong !');
    }
}

function getCurrentQuarterOnCalendar() {
    let curr = new Date();
    loadSpinner();
    let currStartdate = curr.toLocaleDateString();
    $.ajax({
        url: `./employee-scheduler/next-quarter/`,
        data: {
            "startDateStr": currStartdate
        },
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#employeeSchedulerCalendarContainer').empty().html(response);
                initDataTable();
            }
            else {
                showErrorMsg('Something went wrong !');
            }
        },
        error: function (err) {
            showErrorMsg('Something went wrong !');
        },
        complete: function () {
            hideSpinner();
        }
    });
}

function getManageCalendarEmployeeList() {
    loadSpinner();
    $.ajax({
        url: `./employee-scheduler/manage-calendar-employee`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#manageCalendarEmployeeTblContainer').html(response);
                $('#manageCalendarEmployeeModal').modal('show');
            }
            else {
                showErrorMsg('Something went wrong !');
            }
        },
        error: function () {
            showErrorMsg('Something went wrong !');
        },
        complete: function () {
            hideSpinner();
        }
    })
}

function closeManageCalendarEmployeeList() {
    $('#manageCalendarEmployeeModal').modal('hide');
    $('#manageCalendarEmployeeTblContainer').html('');
}

function addEmployeeFromCalendar(employeeId) {
    if (employeeId) {
        loadSpinner();
        $.ajax({
            url: `./employee-scheduler/manage-calendar-employee/${employeeId}/add`,
            method: 'PUT',
            success: function (response) {
                if (typeof response !== undefined && response !== null && response.isSuccess
                    && response.data) {
                    closeManageCalendarEmployeeList();
                    getManageCalendarEmployeeList();
                    getCurrentQuarterOnCalendar();
                    showSuccessMsg(response.message);
                }
                else {
                    showErrorMsg('Something went wrong !');
                }
            },
            error: function () {
                showErrorMsg('Something went wrong !');
            },
            complete: function () {
                hideSpinner();
            }
        })
    }
}

function removeEmployeeFromCalendar(employeeId) {
    if (employeeId) {
        loadSpinner();
        $.ajax({
            url: `./employee-scheduler/manage-calendar-employee/${employeeId}/remove`,
            method: 'PUT',
            success: function (response) {
                if (typeof response !== undefined && response !== null && response.isSuccess && response.data) {
                    getCurrentQuarterOnCalendar();
                    showSuccessMsg(response.message);
                }
                else {
                    showErrorMsg('Something went wrong !');
                }
            },
            error: function () {
                showErrorMsg('Something went wrong !');
            },
            complete: function () {
                hideSpinner();
            }
        })
    }
}

function initDataTable() {
    $('#tblEmployeeSchedulerCalendar').DataTable({
        "ordering": false,
        "paging": false,
        //"scrollCollapse": false,
        "scrollY": '300px',
        "scrollX": true,
        "searching": false,
        "fixedColumns": {
            "left": 5
        },
        "responsive": true,
        //className: ''
    });
}

function getReplaceEmployeeCalendarList(empId) {
    if (empId) {
        loadSpinner();
        $.ajax({
            url: `./employee-scheduler/${empId}/replace`,
            method: 'GET',
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#replaceCalendarEmployeeTblContainer').html(response);
                    $('#replaceCalendarEmployeeModal').modal('show');
                }
                else {
                    showErrorMsg('Something went wrong !');
                }
            },
            error: function () {
                showErrorMsg('Something went wrong !');
            },
            complete: function () {
                hideSpinner();
            }
        })
    }
}

function replaceEmployeeFromCalendarList(oldEmployeeId, newEmployeeId) {
    if (oldEmployeeId && newEmployeeId) {
        loadSpinner();
        $.ajax({
            url: `./employee-scheduler/${oldEmployeeId}/replace/${newEmployeeId}/`,
            method: 'PUT',
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#replaceCalendarEmployeeModal').modal('hide');
                    $('#replaceCalendarEmployeeTblContainer').html('');
                    getCurrentQuarterOnCalendar();
                    showSuccessMsg(response.message);
                }
                else {
                    showErrorMsg('Something went wrong !');
                }
            },
            error: function () {
                showErrorMsg('Something went wrong !');
            },
            complete: function () {
                hideSpinner();
            }
        })
    }
}