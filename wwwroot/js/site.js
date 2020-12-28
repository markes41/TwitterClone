$(document).ready(function(){
    $('.username-content').on('click', function(){
        $('#user').addClass('content-clicked');
        $('#password').removeClass('content-clicked');
    });

    $('.password-content').on('click', function(){
        $('#password').addClass('content-clicked');
        $('#user').removeClass('content-clicked');
    });

    $('.submit').on('click', function(){
        $('#user').removeClass('content-clicked');
        $('#password').removeClass('content-clicked');
    });

    $('.contact-email').hide();
        
    $('.contact-phone-section').on('click', function(){
        $('.contact-phone').hide();
        $('.contact-email').show();
        $('.contact-value').text('Correo electrónico');
        $('#contact-type').get(0).type = 'email';
    });

    $('.contact-email-section').on('click', function(){
        $('.contact-email').hide();
        $('.contact-phone').show();
        $('.contact-value').text('Teléfono');
        $('#contact-type').get(0).type = 'tel';
    });

});