$(document).ready(function () {
    $('#Cover').on('change', function () {
        $('.Cover-Preview').attr('src', window.URL.createObjectURL(this.files[0])).removeClass('d-none');
    });
});