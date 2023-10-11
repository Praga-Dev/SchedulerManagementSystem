$(function () {
    $('#formCreateGrade').validate({
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
            disableBtnById('btnSaveGradeSubmit');

            let id = $('#formCreateGrade').data('id');
            let name = $('#name').val();
            let gradeInfo = {
                'Id': id,
                'Name': name
            }

            let isUpdate = $('#formCreateGrade').data('isupdate') === 'True';
            $.ajax({
                url: isUpdate ? `./grade/update` : `./grade/create`,
                method: isUpdate ? 'PUT' : 'POST',
                data: gradeInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        $('#createGradeModal').modal('hide');
                        showSuccessMsg('Grade saved successfully'); // Add Response from server
                        getGradeList();
                    } else {
                        showErrorMsg('Grade save failed');
                    }
                },
                error: function (error, response) {
                    console.log(response);
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnSaveGradeSubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnSaveGradeSubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
})


$('#formCreateGrade #name').on('focus', function () {
    $('#formCreateGrade  #gradeHelp').show();
});

$('#formCreateGrade #name').on('blur', function () {
    $('#formCreateGrade  #gradeHelp').hide();
});

function saveGrade() {
    $('#btnGradeSaveSubmit').click();
}