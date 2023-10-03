$(document).ready(function () {
    $('[data-val-required]').each(function () {
        $(this).siblings('label').append('<span class="mandatory">*</span>');
    })
});