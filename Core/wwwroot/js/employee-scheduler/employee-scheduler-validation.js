function updateAllocationWorkingHour(event, employeeSchedulerId) {
    if (employeeSchedulerId && event && event.target && event.target.value) {
        debugger;
        loadSpinner();
        let updateAllocationHour = parseInt(event.target.value);
        $.ajax({
            url: `./employee-scheduler/${employeeSchedulerId}/update-allocation-hour/${updateAllocationHour}`,
            method: 'PUT',
            success: function (response) {
                if (typeof response !== undefined && response !== null) {
                    getCurrentQuarterOnCalendar();
                    showSuccessMsg(response.message);

                }
                else {
                    $(this).val($(this).data('curr-val'));
                    showErrorMsg('Something went wrong !');
                }
            },
            error: function (err) {
                $(this).val($(this).data('curr-val'));
                showErrorMsg('Something went wrong !');
            },
            complete: function () {
                hideSpinner();
            }
        })
    } else {
        if (event && event.target) {
            $(event.target).val($(event.target).data('curr-val'));
        } else {
            getCurrentQuarterOnCalendar();
        }
        showErrorMsg('Something went wrong !');
    }
}