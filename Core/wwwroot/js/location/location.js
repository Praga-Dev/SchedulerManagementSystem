function getLocationList() {
    loadSpinner();
    $.ajax({
        url: `./location/list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#locationListContainer').html(response);
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


function onCreateLocation() {
    loadSpinner();
    $('#formCreateLocation').trigger("reset");
    $('#formCreateLocation').data('id', '');
    $('#formCreateLocation').data('isupdate', 'False');
    $('#formCreateLocation').find(':input,select').val('');
    $('#formCreateLocation').find('span.error').hide();
    $('#createLocationTitle').text('Create Location');
    $('#createLocationModal').modal('show');
    hideSpinner();
}


function editLocation(locationInfoId) {
    if (locationInfoId) {
        loadSpinner();
        disableBtnById('btnEditLocation');
        $.ajax({
            url: `./location/${locationInfoId}`,
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#createLocationFormContainer').empty().html(response);
                    $('#createLocationTitle').text('Update Location');
                    $('#createLocationModal').modal('show');
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
                enableBtnById('btnEditLocation');
            }
        })
    } else {
        showErrorMsg('Something went wrong !');
    }
}

function deleteLocation(locationInfoId) {
    if (locationInfoId) {
        loadSpinner();
        $.ajax({
            url: `./location/${locationInfoId}/delete`,
            method: 'DELETE',
            success: function (response) {
                if (typeof response !== undefined && response !== null && response.isSuccess
                    && response.data) {
                    showSuccessMsg('Location deleted successfully');
                    getLocationList();
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
