function getGradeDDList() {
    loadSpinner();
    $.ajax({
        url: `./grade/data-list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#gradeListDDContainer').html(response);
                let val = $('#gradeListDDContainer').data('val')
                if (val) {
                    $('#selectGrade').val(val);
                } else {
                    $('#selectGrade').val('');
                }
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

function getLocationDDList() {
    loadSpinner();
    $.ajax({
        url: `./location/data-list`,
        method: 'GET',
        success: function (response) {
            if (typeof response !== undefined && response !== null) {
                $('#locationListDDContainer').html(response);
                let val = $('#locationListDDContainer').data('val')
                if (val) {
                    $('#selectLocation').val(val);
                } else {
                    $('#selectLocation').val('');
                }
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


// #region Common

function showToast() {
    $('#toast').toast({ delay: 5000 });
    $('#toast').toast('show');
}

function showSuccessMsg(message) {
    if (message) {
        $('#toast:first').attr('class', 'toast primary align-items-center text-white border-0')
        $('#toast #toast-message').text(message);
        showToast();
    }
}

function showErrorMsg(message) {
    if (message) {
        $('#toast:first').attr('class', 'toast bg-danger align-items-center text-white border-0')
        $('#toast #toast-message').text(message);
        showToast();
    }
}

function loadSpinner() {
    $('#loader').show();
    $('main').addClass('pk-backdrop');
}

function hideSpinner() {
    $('#loader').hide();
    $('main').removeClass('pk-backdrop');
}

function disableBtnById(btnId) {
    if (btnId) {
        $('#' + btnId).prop('disabled', true);
    }
}

function enableBtnById(btnId) {
    if (btnId) {
        $('#' + btnId).prop('disabled', false);
    }
}

// #endregion


// #region helpers

function isNotFutureDate(date) {
    if (date) {
        return new Date(date).setHours(0, 0, 0, 0) <= new Date();
    }
}

function isValidDate(d) {
    return d instanceof Date && !isNaN(d);
}

// #endregion