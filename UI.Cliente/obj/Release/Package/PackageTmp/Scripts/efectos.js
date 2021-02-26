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
});
function Apoyar() {
    window.open('https://www.paypal.com/donate?hosted_button_id=REUYFHWBULQZL', 'Apoyar Poeta Muerto', 'width=120,heigth=300,scrollbars=NO');
}
function desaparecerCirculoCarga() {
    //desaparecer la carga
    var contenedor = document.getElementById('contenedor-carga');
    contenedor.style.visibility = 'hidden';
    contenedor.style.opacity = '0';
}
function apoyarPoeta() {
    Swal.fire({
        title: 'Apoyar a Josue Cardona',
        text: 'Seras redirigido a la página de PayPal para efectuar tu ayuda.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Ok, ir a PayPal!',
        cancelButtonText:'Cancelar',
    }).then((result) => {
        if (result.isConfirmed) {
            //document.getElementById('botonPagarPaypal').click();
            Swal.fire({
                position: 'center',
                icon: 'success',
                title: 'Muy pronto estara completa la función de apoyar.',
                showConfirmButton: false,
                timer: 2000
            })
        }
    })
}