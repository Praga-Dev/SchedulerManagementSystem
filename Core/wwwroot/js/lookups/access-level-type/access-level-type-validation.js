$(function () {
    $('#formCreateAccessLevelType').validate({
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
            disableBtnById('btnSaveAccessLevelTypeSubmit');

            let id = $('#formCreateAccessLevelType').data('id');
            let name = $('#name').val();
            let accessLevelTypeInfo = {
                'Id': id,
                'Name': name
            }

            let isUpdate = $('#formCreateAccessLevelType').data('isupdate') === 'True';
            $.ajax({
                url: isUpdate ? `./access-level-type/update` : `./access-level-type/create`,
                method: isUpdate ? 'PUT' : 'POST',
                data: accessLevelTypeInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        $('#createAccessLevelTypeModal').modal('hide');
                        showSuccessMsg('AccessLevelType saved successfully'); // Add Response from server
                        getAccessLevelTypeList();
                    } else {
                        showErrorMsg('AccessLevelType save failed');
                    }
                },
                error: function (error, response) {
                    console.log(response);
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnSaveAccessLevelTypeSubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnSaveAccessLevelTypeSubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
})


$('#formCreateAccessLevelType #name').on('focus', function () {
    $('#formCreateAccessLevelType  #accessLevelTypeHelp').show();
});

$('#formCreateAccessLevelType #name').on('blur', function () {
    $('#formCreateAccessLevelType  #accessLevelTypeHelp').hide();
});

function saveAccessLevelType() {
    $('#btnAccessLevelTypeSaveSubmit').click();
}