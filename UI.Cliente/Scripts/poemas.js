window.onload = function () {
    llamarPoemas();
}
function llamarPoemas() {
    $.get('/poema/poemas', function (data) {
        var html = '';
        html += '<h5 class="col s12 l12 m12">Mi trabajo</h5>';
        $.each(data, function (key, item) {
            html += `
                <div class="col s12 m6 l4">
                <div class="card">
                <div class="card-image waves-effect waves-block waves-light">
                <img class="activator" src="${item.Imagen}">
                </div>
                <div class="card-content">
                <span class="card-title activator grey-text text-darken-4">${item.Titulo}<i class="material-icons right">more_vert</i></span>
                </div>
                <div class="card-reveal">
                <span class="card-title grey-text text-darken-4">${item.Titulo}<i class="material-icons right">close</i></span>
                <p>${item.Verso}</p>
                <div style="flex-wrap:wrap;">
                <a class="waves-effect waves-light btn"><i class="far fa-thumbs-up"></i> Me gusta</a>
                <a href="/PayPal/Index" class="waves-effect waves-light btn"><i class="fab fa-paypal"></i> Apoyar</a>
                </div>
                </div>
                </div>
                </div>`
        })
        document.getElementById('poemas').innerHTML = html;
    });
}