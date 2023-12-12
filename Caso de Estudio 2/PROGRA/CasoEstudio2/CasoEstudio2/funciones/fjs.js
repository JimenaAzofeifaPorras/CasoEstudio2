function ConsultaSaldo() {
    let idCasa = $("#idCasa").val();
    if (idCasa.length > 0) {
        $.ajax({
            url: '/Casa/ConsultaPrecio',
            type: "GET",
            data: { q: idCasa },
            success: function (data) {
                $("#Precio").val(data.PrecioCasa);
            }
        });
    } else {
        $("#Precio").val("");
    }
}
