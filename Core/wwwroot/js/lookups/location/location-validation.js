$(function () {
    $('#formCreateLocation').validate({
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
            disableBtnById('btnSaveLocationSubmit');

            let id = $('#formCreateLocation').data('id');
            let name = $('#name').val();
            let locationInfo = {
                'Id': id,
                'Name': name
            }

            let isUpdate = $('#formCreateLocation').data('isupdate') === 'True';
            $.ajax({
                url: isUpdate ? `./location/update` : `./location/create`,
                method: isUpdate ? 'PUT' : 'POST',
                data: locationInfo,
                success: function (response) {
                    if (typeof response !== undefined && response !== null && response.isSuccess && response.data != null) {
                        $('#createLocationModal').modal('hide');
                        showSuccessMsg('Location saved successfully'); // Add Response from server
                        getLocationList();
                    } else {
                        showErrorMsg('Location save failed');
                    }
                },
                error: function (error, response) {
                    console.log(response);
                    showErrorMsg('Something went wrong');
                },
                complete: function () {
                    hideSpinner();
                    enableBtnById('btnSaveLocationSubmit');
                }
            });
        },
        invalidHandler: function (event, validator) {
            enableBtnById('btnSaveLocationSubmit');
        },
        errorClass: 'error',
        highlight: function (element, errorClass, validClass) { },
        unhighlight: function (element, errorClass, validClass) { }
    });
})


$('#formCreateLocation #name').on('focus', function () {
    $('#formCreateLocation  #locationHelp').show();
});

$('#formCreateLocation #name').on('blur', function () {
    $('#formCreateLocation  #locationHelp').hide();
});

function saveLocation() {
    $('#btnLocationSaveSubmit').click();
}