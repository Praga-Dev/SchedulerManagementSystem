$(function () {
    $('#formCreateTerminationType').validate({
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
            disableBtnById('btnSaveTerminationTypeSubmit');

            let id = $('#formCreateTerminationType').data('id');
            let name = $('#name').val();
            let gradeInfo = {
                'Id': id,
                'Name': name
            }

            let isUpdate = $('#formCreateTerminationType').data('isupdate') === 'True';
            $.ajax({
                url: isUpdate ? `./termination-type/update` : `./termination-type/create`,
                method: isUpdate ? 'PUT' : 'POST',
                data: gradeInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        $('#createTerminationTypeModal').modal('hide');
                        showSuccessMsg('TerminationType saved successfully'); // Add Response from server
                        getTerminationTypeList();
                    } else {
                        showErrorMsg('TerminationType save failed');
                    }
                },
                error: function (error, response) {
                    console.log(response);
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnSaveTerminationTypeSubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnSaveTerminationTypeSubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
})


$('#formCreateTerminationType #name').on('focus', function () {
    $('#formCreateTerminationType  #gradeHelp').show();
});

$('#formCreateTerminationType #name').on('blur', function () {
    $('#formCreateTerminationType  #gradeHelp').hide();
});

function saveTerminationType() {
    $('#btnTerminationTypeSaveSubmit').click();
}