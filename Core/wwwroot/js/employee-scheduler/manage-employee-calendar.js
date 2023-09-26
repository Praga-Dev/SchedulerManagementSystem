$(document).ready(function () {
    $("#searchTextInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#manageCalendarEmployeeTbl tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});

$('#btnActionManageEmpDropDown').on("click", function () {
    let count = getSelectedEmployeesCount();

    if (count > 0) {
        $('#liAllList').hide();
        $('#liSelectedList').show();
    } else {
        $('#liAllList').show();
        $('#liSelectedList').hide();
    }

});



function getSelectedEmployeesCount() {
    return $('.manageEmployeeChkBox:checked').length;
}

function getSelectedEmployeesId() {
    var selectedEmpList = [];

    $('.manageEmployeeChkBox:checked').each(function (i) {
        selectedEmpList.push($(this).val());
    });

    return selectedEmpList;
}

function addSelectedEmployees() {
    employeeIdList = getSelectedEmployeesId();

    if (employeeIdList.length > 0) {

        loadSpinner();
        $.ajax({
            url: `./employee-scheduler/manage-calendar-employee/bulk/add`,
            method: 'PUT',
            data: {
                employeeIdList: employeeIdList
            },
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

function removeSelectedEmployees() {
    debugger;
    employeeIdList = getSelectedEmployeesId();

    if (employeeIdList.length > 0) {

        loadSpinner();
        $.ajax({
            url: `./employee-scheduler/manage-calendar-employee/bulk/remove`,
            method: 'PUT',
            data: {
                employeeIdList: employeeIdList
            },
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

function addAllEmployees() {
    employeeIdList = []

    $('.manageEmployeeChkBox').each(function (i) {
        employeeIdList.push($(this).val());
    });

    if (employeeIdList.length > 0) {

        loadSpinner();
        $.ajax({
            url: `./employee-scheduler/manage-calendar-employee/bulk/add`,
            method: 'PUT',
            data: {
                employeeIdList: employeeIdList
            },
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

function removeAllEmployees() {

    employeeIdList = []

    $('.manageEmployeeChkBox').each(function (i) {
        employeeIdList.push($(this).val());
    });

    if (employeeIdList.length > 0) {

        loadSpinner();
        $.ajax({
            url: `./employee-scheduler/manage-calendar-employee/bulk/remove`,
            method: 'PUT',
            data: {
                employeeIdList: employeeIdList
            },
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