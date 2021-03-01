window.onload = function () {
    llamarPoemas();
}
function llamarPoemas() {
    fetch('/poemapopular/listarPoemasPopulares').then(data => data.json()).then(data => {
        var html = '';
        if (data.length > 0) {
            for (var i = 0; i < data.length; i++) {
                var dataActual = data[i];
                //pintamos la card con sus respectivos datos
                var idGenerado = 'Like' + dataActual['Idpoema'];
                html += `
                <div class="col s12 m6 l4">
                <div class="card">
                <div class="card-image waves-effect waves-block waves-light">
                <img class="activator card-img" src="${dataActual['Imagen']}" alt="${dataActual['Titulo']}" title="${dataActual['Titulo']}">
                </div>
                <div class="card-content">
                <span class="card-title activator grey-text text-darken-4"><i>${dataActual['Titulo']}</i><i class="material-icons right">more_vert</i></span>
                </div>
                <div class="card-reveal">
                <span class="card-title grey-text text-darken-4"><i>${dataActual['Titulo']}</i><i class="material-icons right">close</i></span>
                <i>${dataActual['Verso']}</i>
                <div style="flex-wrap:wrap;">
                <a href="#" onclick="apoyarPoeta()" class="waves-effect waves-light btn"><i class="fab fa-paypal"></i> Apoyar</a>
                <button id="${idGenerado}" class="waves-effect waves-light btn" onclick="addLike(${dataActual['Idpoema']})"></button>
                </div>
                </div>
                </div>
                </div>`

                //contamos los like que tiene la imagen
                var Idpoema = dataActual['Idpoema'];
                contarLikes(Idpoema);
            }
        } else {
            html += '<div class="col s12 m12 l12">'
            html += '<center><p>Actualmente no hay ningun poema popular</p></center>'
            html += '</div">'
        }
        //pintamos las cards
        document.getElementById('poemas').innerHTML = html;
    });
    //hacemos que desaparezca el efecto de carga
    desaparecerCirculoCarga();
}
function addLike(id) {
    var obj = {
        Idpoema: id,
        Ipcliente: null
    }
    fetch('/poema/darMegusta', {
        headers: {
            'Content-Type': 'application/json'
        },
        method: 'POST',
        body: JSON.stringify(obj)
    }).then(res => res.text()).then(res => {
        if (res == 'ok') {
            contarLikes(id);
        } else {
            alert(res);
        }
    });
}
function contarLikes(id) {
    fetch('/poema/contarLike?Idpoema=' + id).then(data => data.json()).then(data => {
        var totalLike = data;
        var button = document.getElementById('Like' + id);
        button.innerHTML = '<i class="fas fa-thumbs-up"></i> ' + totalLike;
    });
}