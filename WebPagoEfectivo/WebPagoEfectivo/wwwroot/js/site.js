var siteUrl = "https://localhost:44308/";
$(document).ready(function () {
    $('#btn-crear').click(function () {
        let errors = [];

        let params = getParametersCrearPromocion();
        validateCrearPromocion(params, errors);

        if (errors.length === 0) {
            $.ajax({
                url: siteUrl + 'api/v1/promociones',
                data: params,
                type: 'POST',
                success: function (data, status, xhr) {
                    showMessage('message_row', "s", [data.message]);
                    $('#txtNombre').val('');
                    $('#txtEmail').val('');
                },
                error: function (xhr) {
                    if (xhr.status === 500) {
                        showMessage('message_row', "e", ['Ocurrio un error de servidor']);
                    } else {
                        let message = $.parseJSON(xhr.responseText).messages;
                        showMessage('message_row', "e", message);
                    }
                }
            });
        } else {
            showMessage('message_row', "i", errors);
        }
    });

    $('#btn-canjear').click(function () {
        let errors = [];

        let params = getParametersCanjearPromocion();
        validateCanjearPromocion(params, errors);

        if (errors.length === 0) {
            $.ajax({
                url: siteUrl + 'api/v1/promociones',
                data: params,
                type: 'PUT',
                success: function (data, status, xhr) {
                    showMessage('message_row', "s", [data.message]);
                    $('#txtCodigo').val('');
                },
                error: function (xhr) {
                    if (xhr.status === 500) {
                        showMessage('message_row', "e", ['Ocurrio un error de servidor']);
                    } else {
                        let message = $.parseJSON(xhr.responseText).messages;
                        showMessage('message_row', "e", message);
                    }
                }
            });
        } else {
            showMessage('message_row', "i", errors);
        }
    });

    loadListadoGlobal();
});
function loadListadoGlobal() {
    $.ajax({
        url: siteUrl + 'api/v1/promociones/getall',
        type: 'GET',
        success: function (data, status, xhr) {
            $('#tb-Promociones >tbody').append(loadHandlebarTemplate('template-filters', { response: data }));
        },
        error: function (xhr) {
            if (xhr.status === 500) {
                showMessage('message_row', "e", ['Ocurrio un error de servidor']);
            } else {
                let message = $.parseJSON(xhr.responseText).messages;
                showMessage('message_row', "e", message);
            }
        }
    });
}
function getParametersCrearPromocion() {
    let params = {
        Email: $('#txtEmail').val(),
        Nombre: $('#txtNombre').val()
    };
    return params;
}
function validateCrearPromocion(params, errors) {
    if (params.Nombre.trim() === '') {
        errors.push("Debe ingresar un Nombre");
    }
    if (params.Email.trim() === '') {
        errors.push("Debe ingresar un Email");
    }
}
function getParametersCanjearPromocion() {
    let params = {
        CodigoGenerado: $('#txtCodigo').val()
    };
    return params;
}
function validateCanjearPromocion(params, errors) {
    if (params.CodigoGenerado.trim() === '') {
        errors.push("Debe ingresar un código");
    }
}
function showMessage(id, type, errors) {
    let contenido = $('div[id$=' + id + ']');
    let result = '';
    contenido.append(errors != null ? $.map(errors, function (error) {
        switch (type) {
            case "s":
                result += '<p class="alert alert-success">' + error + '</p>';
                break;
            case "e":
                result += '<p class="alert alert-danger">' + error + '</p>';
                break;
            case "i":
                result += '<p class="alert alert-warning">' + error + '</p>';
                break;
        }
    }) : null);


    contenido.empty().fadeIn().append(result);
    contenido.delay("20000").fadeOut();
    $('html, body').animate({ scrollTop: $('div[id$=' + id + ']').offset().top }, 'fast');
}
function loadHandlebarTemplate(templateId, jsonObject) {
    let stemplate = $("#" + templateId).html();
    let tmpl = Handlebars.compile(stemplate);
    return tmpl(jsonObject);
}