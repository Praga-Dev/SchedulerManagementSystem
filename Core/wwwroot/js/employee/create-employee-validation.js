let controller = 'employee';

$(function () {
    $('#formCreateEmployee').validate({
        errorElement: 'span',
        rules: {
            name: {
                required: true,
                minlength: 2,
                //maxlength: 25
            }
        },
        messages: {
            name: {
                required: $('#name').data('err-required'),
                minlength: $('#name').data('err-min-length'),
                //maxlength: $('#name').data('err-max-length')
            }
        },
        submitHandler: function () {
            debugger;
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
            let url = `/${controller}/`;

            url += isUpdate ? 'update' : 'create';

            $.ajax({
                url: url, 
                method: isUpdate ? 'PUT' : 'POST',
                data: employeeInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        showSuccessMsg(response.message); // Add Response from server

                        // TODO go to employee list
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
    $('#formCreateEmployee').submit();
}