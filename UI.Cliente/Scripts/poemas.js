window.onload = function () {
    llamarPoemas();
}
function llamarPoemas() {
    fetch('/poema/poemas').then(data => data.json()).then(data => {
        var html = '';
        for (var i = 0; i < data.length; i++) {
            //verificamos si la imagenen no viene vacia
            var dataActual = data[i]; var urlImagen = '';
            if (dataActual['Imagen'] == null || dataActual['Imagen'] == '') {
                urlImagen = '/Content/img/J.png';
            } else {
                urlImagen = dataActual['Imagen']
            }
            //pintamos la card con sus respectivos datos
            var idGenerado = 'Like' + dataActual['Idpoema'];
            html += `
                <div class="col s12 m6 l4">
                <div class="card">
                <div class="card-image waves-effect waves-block waves-light">
                <img class="activator card-img" src="${urlImagen}">
                </div>
                <div class="card-content">
                <span class="card-title activator grey-text text-darken-4"><i>${dataActual['Titulo']}</i><i class="material-icons right">more_vert</i></span>
                </div>
                <div class="card-reveal">
                <span class="card-title grey-text text-darken-4"><i>${dataActual['Titulo']}</i><i class="material-icons right">close</i></span>
                <p>${dataActual['Verso']}</p>
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
        //pintamos las cards
        document.getElementById('poemas').innerHTML = html;
        //hacemos que desaparezca el efecto de carga
        desaparecerCirculoCarga();
    });
}

function addLike(id) {
    //objeto
    var MeGusta = {
        Idpoema: id,
        Ipcliente: null
    }
    //peticion
    fetch("/poema/addlike", {
        headers: {
            "Content-Type": "application/json"
        },
        method: "POST",
        body: JSON.stringify(MeGusta)
    }).then(res => res.text()).then(res => {
        if (res == "ok") {
            contarLikes(id);
        } else {
            console.log('error: ' + res)
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