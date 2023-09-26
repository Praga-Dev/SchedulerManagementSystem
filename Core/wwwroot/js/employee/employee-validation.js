$(function () {
    $('#formCreateEmployee').validate({
        errorElement: 'span',
        rules: {
            name: {
                required: true,
                minlength: 2,
            }
        },
        messages: {
            name: {
                required: $('#name').data('err-required'),
                minlength: $('#name').data('err-min-length')
            }
        },
        submitHandler: function () {
            loadSpinner();
            disableBtnById('btnSaveEmployeeSubmit');

            let id = $('#formCreateEmployee').data('id');
            let name = $('#employeeName').val();
            let gradeId = $('#selectGrade').val();
            let locationId = $('#selectLocation').val();
            let employeeInfo = {
                'Id': id,
                'Name': name,
                'GradeInfo': {
                    'Id': gradeId
                },
                'LocationInfo': {
                    'Id': locationId
                },
            }

            let isUpdate = $('#formCreateEmployee').data('isupdate') === 'True';
            $.ajax({
                url: isUpdate ? `./employee/update` : `./employee/create`,
                method: isUpdate ? 'PUT' : 'POST',
                data: employeeInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        $('#createEmployeeModal').modal('hide');
                        showSuccessMsg('Employee saved successfully'); // Add Response from server
                        getEmployeeList();
                    } else {
                        showErrorMsg('Employee save failed');
                    }
                },
                error: function (error, response) {
                    console.log(response);
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnSaveEmployeeSubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnSaveEmployeeSubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
})


$('#formCreateEmployee #employeeName').on('focus', function () {
    $('#formCreateEmployee  #employeeNameHelp').show();
});

$('#formCreateEmployee #employeeName').on('blur', function () {
    $('#formCreateEmployee  #employeeNameHelp').hide();
});

function saveEmployee() {
    $('#btnEmployeeSaveSubmit').click();
}