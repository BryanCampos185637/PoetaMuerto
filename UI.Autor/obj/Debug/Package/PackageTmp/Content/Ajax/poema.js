$(function () {
    llamarTabla(undefined);
})
var vista = true;
function llamarTabla(url = '/Poema/listar') {
    pintarTabla(url, ['No', 'titulo', 'verso'], ['Idpoema', 'Titulo', 'Verso'],
        'Idpoema', 'myTable', true, true);
}
function guardar() {
    if (validarCamposVacios()) {
        var frm = new FormData();
        capturarDatos(frm);
        frm.append('Imagen', document.getElementById('IMAGEN').src);
        enviarDatosAlControlador('/poema/guardar', frm, undefined, undefined, function () {
            limpiarFormulario();
            llamarTabla();
            document.getElementById('IMAGEN').src = '';
            cambiarVista();
        });
    } else {
        messeges('warning','Debes completar los campos marcados');
    }
}
function editar(id) {
    cambiarVista();
    llenarFormulario('/poema/obtener?id=' + id, ['Idpoema', 'Titulo','Verso','Imagen']);
}
function eliminar(id) {
    messegeConfirm('Eliminar poema!', 'Estas seguro que deseas eliminar este poema?',
        'warning', 'Si, eliminar!', function () {
            eliminarRegistro('/poema/eliminar?id=' + id, 'Poema eliminado', function () {
                llamarTabla();
            });
        });
}
document.getElementById('txtFileFoto').onchange = function () {
    var file = document.getElementById("txtFileFoto").files[0];//capturamos el archivo
    var reader = new FileReader();//leemos el archivo
    if (reader != null) {//verificamos que no sea null
        reader.onloadend = function () {//cuando termine de cargar
            var img = document.getElementById("IMAGEN");//capturamos el tag de la foto
            img.src = reader.result;//y le asignamos la ruta
        }
        reader.readAsDataURL(file);//convierte a base64
    }
};
document.getElementById('btnFoto').onclick = function () {
    document.getElementById('txtFileFoto').click();
}
function cambiarVista() {
    document.getElementById('Filtro').value = '';
    if (vista) {
        document.getElementById('divFormulario').style.display = 'block';
        document.getElementById('myTable').style.display = 'none';
        document.getElementById('Filtro').style.display = 'none';
        vista = false;
        document.getElementById('btnCambiar').value = 'Mostrar tabla';
    } else {
        document.getElementById('divFormulario').style.display = 'none';
        document.getElementById('myTable').style.display = 'block';
        document.getElementById('Filtro').style.display = 'block';
        document.getElementById('btnCambiar').value = 'Mostrar formulario';
        vista = true;
    }
}
function filtrar() {
    var titulo = document.getElementById('txtFiltro').value;
    llamarTabla('/Poema/listar?Titulo=' + titulo);
}