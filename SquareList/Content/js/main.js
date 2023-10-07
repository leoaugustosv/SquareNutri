jQuery(document).ready(function($) {

    $(window).scroll(function() {
        var height = $(window).height();
        var scroll = $(window).scrollTop();
        if (scroll) {
            $(".header-hide").addClass("scroll-header");
        } else {
            $(".header-hide").removeClass("scroll-header");
        }


    });

    // popovers init

    $(function() {
        $('[data-toggle="popover"]').popover()
    });

    $(function() {
        $('[data-toggle="popover-hover"]').popover({
            html: true,
            trigger: 'hover',
            placement: 'top',
        })

    });



    // Back to top button
    $(window).scroll(function() {
        if ($(this).scrollTop() > 100) {
            $('.back-to-top').fadeIn('slow');
        } else {
            $('.back-to-top').fadeOut('slow');
        }
    });
    $('.back-to-top').click(function() {
        $('html, body').animate({
            scrollTop: 0
        }, 1500, 'easeInOutExpo');
        return false;
    });

    // Initiate the wowjs animation library
    new WOW().init();

    // Initiate superfish on nav menu
    $('.nav-menu').superfish({
        animation: {
            opacity: 'show'
        },
        speed: 400
    });

    // Mobile Navigation
    if ($('#nav-menu-container').length) {
        var $mobile_nav = $('#nav-menu-container').clone().prop({
            id: 'mobile-nav'
        });
        $mobile_nav.find('> ul').attr({
            'class': '',
            'id': ''
        });
        $('body').append($mobile_nav);
        $('body').prepend('<button type="button" id="mobile-nav-toggle"><i class="fa fa-bars"></i></button>');
        $('body').append('<div id="mobile-body-overly"></div>');
        $('#mobile-nav').find('.menu-has-children').prepend('<i class="fa fa-chevron-down"></i>');

        $(document).on('click', '.menu-has-children i', function(e) {
            $(this).next().toggleClass('menu-item-active');
            $(this).nextAll('ul').eq(0).slideToggle();
            $(this).toggleClass("fa-chevron-up fa-chevron-down");
        });

        $(document).on('click', '#mobile-nav-toggle', function(e) {
            $('body').toggleClass('mobile-nav-active');
            $('#mobile-nav-toggle i').toggleClass('fa-times fa-bars');
            $('#mobile-body-overly').toggle();
        });

        $(document).click(function(e) {
            var container = $("#mobile-nav, #mobile-nav-toggle");
            if (!container.is(e.target) && container.has(e.target).length === 0) {
                if ($('body').hasClass('mobile-nav-active')) {
                    $('body').removeClass('mobile-nav-active');
                    $('#mobile-nav-toggle i').toggleClass('fa-times fa-bars');
                    $('#mobile-body-overly').fadeOut();
                }
            }
        });
    } else if ($("#mobile-nav, #mobile-nav-toggle").length) {
        $("#mobile-nav, #mobile-nav-toggle").hide();
    }

    // Smooth scroll for the menu and links with .scrollto classes
    $('.nav-menu a, #mobile-nav a, .scrollto').on('click', function() {
        if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
            var target = $(this.hash);
            if (target.length) {
                var top_space = 0;

                if ($('#header').length) {
                    top_space = $('#header').outerHeight();

                    if (!$('#header').hasClass('header-fixed')) {
                        top_space = top_space - 20;
                    }
                }

                $('html, body').animate({
                    scrollTop: target.offset().top - top_space
                }, 1500, 'easeInOutExpo');

                if ($(this).parents('.nav-menu').length) {
                    $('.nav-menu .menu-active').removeClass('menu-active');
                    $(this).closest('li').addClass('menu-active');
                }

                if ($('body').hasClass('mobile-nav-active')) {
                    $('body').removeClass('mobile-nav-active');
                    $('#mobile-nav-toggle i').toggleClass('fa-times fa-bars');
                    $('#mobile-body-overly').fadeOut();
                }
                return false;
            }
        }
    });

    // Modal video
    new ModalVideo('.js-modal-btn', {
        channel: 'youtube'
    });

    // Init Owl Carousel
    $('.owl-carousel').owlCarousel({
        items: 4,
        autoplay: true,
        loop: true,
        margin: 30,
        dots: true,
        responsiveClass: true,
        responsive: {

            320: {
                items: 1
            },
            480: {
                items: 2
            },
            600: {
                items: 2
            },
            767: {
                items: 3
            },
            768: {
                items: 3
            },
            992: {
                items: 4
            }
        }
    });


//==============================================
// Modal de Reembolso
//==============================================


    // Pegar ID da Modal
    var modal = document.getElementById("refundModal");

    // Pegar ID do botão que abre a Modal
    var btn = document.getElementById("refundMore");

    // Pegar Class que fecha a Modal
    var span = document.getElementsByClassName("close")[0];

    // Abrir Modal ao clicar no botão
    btn.onclick = function() {
        modal.style.display = "block";
    }

    // Fechar Modal ao clicar no X
    span.onclick = function() {
        modal.style.display = "none";
    }

    // Fechar Modal ao clicar em qualquer área fora
    window.onclick = function(event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }


});




//==============================================
// Mostrar Form de Data e Hora na Consulta
//==============================================

$(function() {
    $('#selectNutri').change(function() {
        $('.datahoraForm').fadeIn(2000);
    });
});

//==============================================
// Mostrar Form Marcar Consulta Master
//==============================================

$(function() {
    $('#selectCliente').change(function() {
        $('.selectNutriForm').fadeIn(2000);
    });
});

//==============================================
// Mostrar Form de Pagamento de Cartão na Consulta
//==============================================

$(function() {
    $('#selectPagamento').change(function() {
        $('.cartao-form').fadeOut("slow");

        $('#' + $(this).val()).fadeIn(1500);
    });
});


//==============================================
// Mudar Required para o Cartão na Consulta
//==============================================

function cartaoFalse() {
$(".cartaoPag").attr('required', false);
}
function cartaoTrue() {

$(".cartaoPag").attr('required', true);
}

//==============================================
// Mostrar Forms de acordo com o Plano
//==============================================

$(function() {
    $('#selectPlano').change(function() {
		$('.showPlanos').fadeIn(2000);
		$('#formCartaoPlano').fadeIn(2000);
        $('.planoDiv').fadeOut(1000);
		$('#planoValue').fadeOut(1000);
		
		if ( this.value == 'Square Personal')
      {
        $(".planoPersonal").fadeIn(1000);
		$(".planoPremium").fadeOut(1000);
		
      }
      else
      {
		$(".planoPremium").fadeIn(1000);
        $(".planoPersonal").fadeOut(1000);
      }
	  
        $('#' + $(this).val()).fadeIn(1000);
    });
});

//==============================================
// Mostrar Forms da Listagem de Consultas
//==============================================

$(function() {
    $('#selectListar').change(function() {
		
		
		if ( this.value == "listAll")
      {
        $('.listagemDeConsultas').fadeOut(1000);
		$("#listAll").fadeIn(1000);
		
      }
	  
      else if ( this.value == "listFromDate")
      {
		$('.listagemDeConsultas').fadeOut(1000);
        $("#listFromDate").fadeIn(1000);
      }
	  
	  else if ( this.value == "listFromUser")
	  {
		$('.listagemDeConsultas').fadeOut(1000);
        $("#listFromUser").fadeIn(1000);
      }
	  
	  else{
	alert("Erro");
	}
	  
	});
});




//==============================================
// Mensagem de Confirmação Newsletter e Contato
//==============================================
function newsletterMsg() {
	var x=document.formNewsletter.email.value;  
var atposition=x.indexOf("@");  
var dotposition=x.lastIndexOf(".");  

if (atposition<1 || dotposition<atposition+2 || dotposition+2>=x.length){  
  alert("Por favor insira um email!");  
  return false;  
  }
else{
	alert("Pronto! Agora é só esperar pelas melhores notícias e eventos do mundo da nutrição esportiva!");
  }
}  


function contatoMsg() {
    alert("Mensagem enviada com sucesso! Logo responderemos diretamente para o seu email. ");
}

//================================
// Data de Hoje no Input como Min
//================================

var today = new Date();
var dd = today.getDate();
var mm = today.getMonth()+1; //Janeiro = 0!
var yyyy = today.getFullYear();
 if(dd<10){
        dd='0'+dd
    } 
    if(mm<10){
        mm='0'+mm
    } 

today = yyyy+'-'+mm+'-'+dd;
document.getElementById("dataConsulta").setAttribute("min", today);


//================================
// 2 Meses a frente no Input como Max
//================================

var today = new Date();
var dd = today.getDate();
var mm = ((today.getMonth()+1)+1); //Janeiro = 0!
var yyyy = today.getFullYear();
 if(dd<10){
        dd='0'+dd
    } 
    if(mm<10){
        mm='0'+mm
    } 

today = yyyy+'-'+mm+'-'+dd;
document.getElementById("dataConsulta").setAttribute("max", today);


//============================
//  Jquery Masks
//============================


		$(document).ready(function () { 
        var $CampoCpf = $("#cpf");
        $CampoCpf.mask("000.000.000-00");
		});
		
        $(document).ready(function () {
            $("#txt-data").mask("99/99/9999");
        });

        $(document).ready(function () {
            $("#cod-seg").mask("000");
        });

        $(document).ready(function () {
            $("#cartao-seg").mask("0000 0000 0000 0000");
        });


$(document).ready(function () {
    $('#fone').mask('(00) 00000-0009');
    $('#fone').blur(function (event) {
        if ($(this).val().length == 15) { // Celular com 9 dígitos + 2 dígitos DDD e 4 da máscara
            $('#fone').mask('(00) 00000-0009');
        }
        else {
            $('#fone').mask('(00) 0000-00009');
        }
    })
});

$(document).ready(function () {
    $('#fone1').mask('(00) 00000-0009');
    $('#fone1').blur(function (event) {
        if ($(this).val().length == 15) { // Celular com 9 dígitos + 2 dígitos DDD e 4 da máscara
            $('#fone1').mask('(00) 00000-0009');
        }
        else {
            $('#fone1').mask('(00) 0000-00009');
        }
    })
});