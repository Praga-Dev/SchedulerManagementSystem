function getGradeList() {
    loadSpinner();
    $.ajax({
        url: `./grade/list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#gradeListContainer').html(response);
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


function onCreateGrade() {
    loadSpinner();
    $('#formCreateGrade').trigger("reset");
    $('#formCreateGrade').data('id', '');
    $('#formCreateGrade').data('isupdate', 'False');
    $('#formCreateGrade').find(':input,select').val('');
    $('#formCreateGrade').find('span.error').hide();
    $('#createGradeTitle').text('Create Grade');
    $('#createGradeModal').modal('show');
    hideSpinner();
}


function editGrade(gradeInfoId) {
    if (gradeInfoId) {
        loadSpinner();
        disableBtnById('btnEditGrade');
        $.ajax({
            url: `./grade/${gradeInfoId}`,
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    $('#createGradeFormContainer').empty().html(response);
                    $('#createGradeTitle').text('Update Grade');
                    $('#createGradeModal').modal('show');
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
                enableBtnById('btnEditGrade');
            }
        })
    } else {
        showErrorMsg('Something went wrong !');
    }
}

function deleteGrade(gradeInfoId) {
    if (gradeInfoId) {
        loadSpinner();
        $.ajax({
            url: `./grade/${gradeInfoId}/delete`,
            method: 'DELETE',
            success: function (response) {
                if (typeof response !== undefined && response !== null && response.isSuccess
                    && response.data) {
                    showSuccessMsg('Grade deleted successfully');
                    getGradeList();
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
