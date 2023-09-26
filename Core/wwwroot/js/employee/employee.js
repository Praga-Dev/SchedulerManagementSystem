function getEmployeeList() {
    loadSpinner();
    $.ajax({
        url: `./employee/list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#employeeListContainer').html(response);
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


function onCreateEmployee() {
    loadSpinner();
    getGradeDDList();
    getLocationDDList();
    $('#formCreateEmployee').trigger("reset");
    $('#formCreateEmployee').data('id', '');
    $('#formCreateEmployee').data('isupdate', 'False');
    $('#formCreateEmployee').find(':input,select').val('');
    $('#formCreateEmployee').find('span.error').hide();
    $('#createEmployeeTitle').text('Create Employee');
    $('#createEmployeeModal').modal('show');
    hideSpinner();
}


function editEmployee(employeeInfoId) {
    if (employeeInfoId) {
        loadSpinner();
        disableBtnById('btnEditEmployee');
        $.ajax({
            url: `./employee/${employeeInfoId}`,
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#createEmployeeFormContainer').empty().html(response);
                    $('#createEmployeeTitle').text('Update Employee');
                    $('#createEmployeeModal').modal('show');
                    getGradeDDList();
                    getLocationDDList();
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
                enableBtnById('btnEditEmployee');
            }
        })
    } else {
        showErrorMsg('Something went wrong !');
    }
}

function deleteEmployee(employeeInfoId) {
    if (employeeInfoId) {
        loadSpinner();
        $.ajax({
            url: `./employee/${employeeInfoId}/delete`,
            method: 'DELETE',
            success: function (response) {
                if (typeof response !== undefined && response !== null && response.isSuccess
                    && response.data) {
                    showSuccessMsg('Employee deleted successfully');
                    getEmployeeList();
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
