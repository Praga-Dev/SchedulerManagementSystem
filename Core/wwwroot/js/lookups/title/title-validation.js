$(function () {
    $('#formCreateTitle').validate({
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
            disableBtnById('btnSaveTitleSubmit');

            let id = $('#formCreateTitle').data('id');
            let name = $('#name').val();
            let titleInfo = {
                'Id': id,
                'Name': name
            }

            let isUpdate = $('#formCreateTitle').data('isupdate') === 'True';
            $.ajax({
                url: isUpdate ? `./title/update` : `./title/create`,
                method: isUpdate ? 'PUT' : 'POST',
                data: titleInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        $('#createTitleModal').modal('hide');
                        showSuccessMsg('Title saved successfully'); // Add Response from server
                        getTitleList();
                    } else {
                        showErrorMsg('Title save failed');
                    }
                },
                error: function (error, response) {
                    console.log(response);
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnSaveTitleSubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnSaveTitleSubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
})


$('#formCreateTitle #name').on('focus', function () {
    $('#formCreateTitle  #titleHelp').show();
});

$('#formCreateTitle #name').on('blur', function () {
    $('#formCreateTitle  #titleHelp').hide();
});

function saveTitle() {
    $('#btnTitleSaveSubmit').click();
}