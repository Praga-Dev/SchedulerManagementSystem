function getTerminationTypeList() {
    loadSpinner();
    $.ajax({
        url: `./termination-type/list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#terminationTypeListContainer').html(response);
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


function onCreateTerminationType() {
    loadSpinner();
    $('#formCreateTerminationType').trigger("reset");
    $('#formCreateTerminationType').data('id', '');
    $('#formCreateTerminationType').data('isupdate', 'False');
    $('#formCreateTerminationType').find(':input,select').val('');
    $('#formCreateTerminationType').find('span.error').hide();
    $('#createTerminationTypeTitle').text('Create TerminationType');
    $('#createTerminationTypeModal').modal('show');
    hideSpinner();
}


function editTerminationType(terminationTypeInfoId) {
    if (terminationTypeInfoId) {
        loadSpinner();
        disableBtnById('btnEditTerminationType');
        $.ajax({
            url: `./termination-type/${terminationTypeInfoId}`,
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#createTerminationTypeFormContainer').empty().html(response);
                    $('#createTerminationTypeTitle').text('Update TerminationType');
                    $('#createTerminationTypeModal').modal('show');
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
                enableBtnById('btnEditTerminationType');
            }
        })
    } else {
        showErrorMsg('Something went wrong !');
    }
}

function deleteTerminationType(terminationTypeInfoId) {
    if (terminationTypeInfoId) {
        loadSpinner();
        $.ajax({
            url: `./termination-type/${terminationTypeInfoId}/delete`,
            method: 'DELETE',
            success: function (response) {
                if (typeof response !== undefined && response !== null && response.isSuccess
                    && response.data) {
                    showSuccessMsg('TerminationType deleted successfully');
                    getTerminationTypeList();
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
