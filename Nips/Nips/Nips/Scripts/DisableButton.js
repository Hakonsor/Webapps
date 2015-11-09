$(document).ready(function () {
    validate();
    $('#inputFornavn, #inputEtternavn, #inputKortnummer, #inputCVV, #inputUtlopsdato').change(validate);
});

function validate() {
    if ($('#inputFornavn').val().length > 0 &&
        $('#inputEtternavn').val().length > 0 &&
        $('#inputKortnummer').val().length > 0 &&
        $('#inputCVV').val().length > 0 &&
        $('#inputUtlopsdato').val().length > 0) {
        $("input[type=submit]").prop("disabled", false);
    }
    else {
        $("input[type=submit]").prop("disabled", true);
    }
}