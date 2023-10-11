function getAccessLevelTypeList() {
    loadSpinner();
    $.ajax({
        url: `./access-level-type/list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#accessLevelTypeListContainer').html(response);
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


function onCreateAccessLevelType() {
    loadSpinner();
    $('#formCreateAccessLevelType').trigger("reset");
    $('#formCreateAccessLevelType').data('id', '');
    $('#formCreateAccessLevelType').data('isupdate', 'False');
    $('#formCreateAccessLevelType').find(':input,select').val('');
    $('#formCreateAccessLevelType').find('span.error').hide();
    $('#createAccessLevelTypeTitle').text('Create AccessLevelType');
    $('#createAccessLevelTypeModal').modal('show');
    hideSpinner();
}


function editAccessLevelType(accessLevelTypeInfoId) {
    if (accessLevelTypeInfoId) {
        loadSpinner();
        disableBtnById('btnEditAccessLevelType');
        $.ajax({
            url: `./access-level-type/${accessLevelTypeInfoId}`,
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#createAccessLevelTypeFormContainer').empty().html(response);
                    $('#createAccessLevelTypeTitle').text('Update AccessLevelType');
                    $('#createAccessLevelTypeModal').modal('show');
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
                enableBtnById('btnEditAccessLevelType');
            }
        })
    } else {
        showErrorMsg('Something went wrong !');
    }
}

function deleteAccessLevelType(accessLevelTypeInfoId) {
    if (accessLevelTypeInfoId) {
        loadSpinner();
        $.ajax({
            url: `./access-level-type/${accessLevelTypeInfoId}/delete`,
            method: 'DELETE',
            success: function (response) {
                if (typeof response !== undefined && response !== null && response.isSuccess
                    && response.data) {
                    showSuccessMsg('AccessLevelType deleted successfully');
                    getAccessLevelTypeList();
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
