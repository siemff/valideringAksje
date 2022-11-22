function validerOgRegisterBruker() {
    const fornavnOK = validerFornavn($("#fornavn").val());
    const etternavnOK = validerEtternavn($("#etternavn").val());
    const brukernavnOK = validerBrukernavn($("#brukernavn").val());
    const passordOK = validerPassord($("#passord").val());
    if (fornavnOK && etternavnOK && brukernavnOK && passordOK) {
        registerBruker();
    }
}

function registerBruker() {
    const bruker = {
        fornavn: $("#fornavn").val(),
        etternavn: $("#etternavn").val(),
        brukernavn: $("#brukernavn").val(),
        passord: $("#passord").val(),
    }
    const url = "aksje/Register";
    $.post(url, bruker, function () {
        window.location.href = 'index.html';
    })
        .fail(function () {
            $("#feil").html("Feil på server - prøv igjen senere");
        });
};