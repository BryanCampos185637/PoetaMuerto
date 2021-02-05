function pintarTabla(url, cabecera, propiedades, llavePrimaria, idDiv = 'myTable', eliminar = true, editar = true) {
    fetch(url).then(data => data.json()).then(data => {
        var totalColumnas = cabecera.length; var html = '';
        html += '<table class="table table-bordered table-hover table-responsive">'
        html += '<thead class="thead-dark">'
        html += '<tr>'
        for (var i = 0; i < cabecera.length; i++) {
            html += '<th>' + cabecera[i].toUpperCase() + '</th>'
        }
        if (eliminar || editar) {
            totalColumnas++;
            html += '<th>OPCIONES</th>'
        }
        html += '</tr>'
        html += '</thead>'
        html += '<tbody>'
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                var filaActual = data[i];
                html += '<tr>'
                for (var j = 0; j < propiedades.length; j++) {
                    var propiedadActual = propiedades[j];
                    html += '<td>' + filaActual[propiedadActual] + '</td>';
                }
                html += '<td width="15%">'
                if (eliminar) {
                    html += '<button class="btn-sm btn-danger" onclick="eliminar(' + filaActual[llavePrimaria] + ')">eliminar</button> '
                }
                if (editar) {
                    html += '<button class="btn-sm btn-success" onclick="editar(' + filaActual[llavePrimaria] + ')">editar</button>'
                }
                html += '</td>'
                html += '</tr>'
            }
        } else {
            html += '<tr>'
            html += '<td class="text-center" colspan="' + totalColumnas + '">No hay ningun registro</td>'
            html += '</tr>'
        }
        html += '</tbody>'
        html += '</table>'
        document.getElementById(idDiv).innerHTML = html;
    })
}
function enviarDatosAlControlador(url, datos, mensajeExito = 'Exito', mensajeRepetido = 'Este registro ya existe', collback){
    $.ajax({
        url: url,
        type: 'POST',
        contentType: false,
        processData: false,
        data: datos,
        success: function (rpt) {
            if (rpt == 'ok') {
                messeges('success', mensajeExito);
                collback();
            } else if (rpt == 'repetido') {
                messeges('warning', mensajeRepetido);
            } else {
                messeges('error', rpt);
            }
        }
    });
}
function capturarDatos(frm) {
    var inputs = document.getElementsByClassName('data');
    for (var i = 0; i < inputs.length; i++) {
        frm.append(inputs[i].name, inputs[i].value);
    }
}
function validarCamposVacios() {
    var rpt = true;
    var inputsValidar = document.getElementsByClassName('requerid');
    for (var i = 0; i < inputsValidar.length; i++) {
        if (inputsValidar[i].value.trim() == '') {
            rpt = false;
            inputsValidar[i].style.borderColor = 'red';
        } else {
            inputsValidar[i].style.borderColor = '#ccc';
        }
    }
    return rpt;
}
function limpiarFormulario() {
    var inputs = document.getElementsByClassName('data');
    for (var i = 0; i < inputs.length; i++) {
        inputs[i].value = '';
        inputs[i].style.borderColor = '#ccc';
    }
}
function eliminarRegistro(url, mensajeExito = 'Exito', collback) {
    $.get(url, function (rpt) {
        if (rpt == 'ok') {
            messeges('success',mensajeExito);
            collback();
        } else {
            messeges('error',rpt);
        }
    })
}
function llenarFormulario(url, propiedades) {
    $.get(url, function (data) {
        for (var i = 0; i < propiedades.length; i++) {
            var propiedadActual = propiedades[i];
            if (propiedadActual != 'Imagen') {
                document.getElementById(propiedades[i]).value = data[propiedadActual];
            } else {
                document.getElementById('IMAGEN').src = data[propiedadActual];
            }
        }
    })
}
function llenarSelect(url, mensajeInicio = '--Selecciona una opción--', llavePrimaria, mensaje, idSelect) {
    $.get(url, function (data) {
        var html = '';
        html += '<option value="">' + mensajeInicio + '</option>';
        for (var i = 0; i < data.length;i++) {
            var dataActual = data[i];
            html += '<option value="' + dataActual[llavePrimaria] + '">' + dataActual[mensaje] + '</option>';
        }
        document.getElementById(idSelect).innerHTML = html;
    });
}
function validarSiEsNumero(input) {
    rpt = true;
    if (isNaN(input.value)) {
        rpt = false;
        input.style.borderColor = 'red';
    } else {
        input.style.borderColor = '#ccc';
    }
    return rpt;
}
function messeges(icons = 'success', titles = 'Exito') {
    Swal.fire({
        position: 'center',
        icon: icons,
        title: titles,
        showConfirmButton: false,
        timer: 2000
    })
}

function messegeConfirm(titulo = 'Elminiar', texto = '¿Estas seguro que deseas eliminar el registro?',
    icono ='warning',tituloBoton = 'Si, eliminar!', collback) {
    Swal.fire({
        title: titulo,
        text: texto,
        icon: icono,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: tituloBoton
    }).then((result) => {
        if (result.isConfirmed) {
            collback();
        }
    })
}