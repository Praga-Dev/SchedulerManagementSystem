$(document).ready(function () {
    $("#searchTextInput").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#manageCalendarEmployeeTbl tbody tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});