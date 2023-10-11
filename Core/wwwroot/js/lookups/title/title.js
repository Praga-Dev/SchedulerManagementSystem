function getTitleList() {
    loadSpinner();
    $.ajax({
        url: `./title/list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#titleListContainer').html(response);
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


function onCreateTitle() {
    loadSpinner();
    $('#formCreateTitle').trigger("reset");
    $('#formCreateTitle').data('id', '');
    $('#formCreateTitle').data('isupdate', 'False');
    $('#formCreateTitle').find(':input,select').val('');
    $('#formCreateTitle').find('span.error').hide();
    $('#createTitleTitle').text('Create Title');
    $('#createTitleModal').modal('show');
    hideSpinner();
}


function editTitle(titleInfoId) {
    if (titleInfoId) {
        loadSpinner();
        disableBtnById('btnEditTitle');
        $.ajax({
            url: `./title/${titleInfoId}`,
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#createTitleFormContainer').empty().html(response);
                    $('#createTitleTitle').text('Update Title');
                    $('#createTitleModal').modal('show');
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
                enableBtnById('btnEditTitle');
            }
        })
    } else {
        showErrorMsg('Something went wrong !');
    }
}

function deleteTitle(titleInfoId) {
    if (titleInfoId) {
        loadSpinner();
        $.ajax({
            url: `./title/${titleInfoId}/delete`,
            method: 'DELETE',
            success: function (response) {
                if (typeof response !== undefined && response !== null && response.isSuccess
                    && response.data) {
                    showSuccessMsg('Title deleted successfully');
                    getTitleList();
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
