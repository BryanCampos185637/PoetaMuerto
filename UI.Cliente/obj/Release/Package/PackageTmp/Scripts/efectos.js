document.addEventListener('DOMContentLoaded', function () {
    var elems = document.querySelectorAll('.sidenav');
    var instances = M.Sidenav.init(elems,);
});

$(document).ready(function () {
    //evento del boton
    $('#ir-arriba').click(function () {
        $('body,html').animate({
            scrollTop: '0px'//hasta donde subir
        },300/*cuanto deberia tardar*/);
    });
    //aparezca o desaparezca el boton
    $(window).scroll(function () {
        if ($(this).scrollTop() > 0) {
            $('#ir-arriba').slideDown(300);
        } else {
            $('#ir-arriba').slideUp(300);
        }
    });
    //desaparecer la carga
    var contenedor = document.getElementById('contenedor-carga');
    contenedor.style.visibility = 'hidden';
    contenedor.style.opacity = '0';
});