window.onload = function () {
    llamarPoemas();
}
function llamarPoemas() {
    $.get('/poema/poemas', function (data) {
        var html = '';
        $.each(data, function (key, item) {
            var urlImagen = '';
            if (item.Imagen == null || item.Imagen == '') {
                urlImagen = '/Content/img/J.png';
            } else {
                urlImagen = item.Imagen
            }
            html += `
                <div class="col s12 m6 l4">
                <div class="card">
                <div class="card-image waves-effect waves-block waves-light">
                <img class="activator card-img" src="${urlImagen}">
                </div>
                <div class="card-content">
                <span class="card-title activator grey-text text-darken-4"><i>${item.Titulo}</i><i class="material-icons right">more_vert</i></span>
                </div>
                <div class="card-reveal">
                <span class="card-title grey-text text-darken-4"><i>${item.Titulo}</i><i class="material-icons right">close</i></span>
                <i>${item.Verso}</i>
                <div style="flex-wrap:wrap;">
                <a href="/PayPal/Index" class="waves-effect waves-light btn"><i class="fab fa-paypal"></i> Apoyar</a>
                </div>
                </div>
                </div>
                </div>`
        })
        document.getElementById('poemas').innerHTML = html;
    });
    desaparecerCirculoCarga()
}