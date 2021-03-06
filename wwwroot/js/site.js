﻿$(document).ready(function(){
    
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

    var test1 = $('#test').val();
    $('.following-window-follow-btn').hide();
    
    if(test1 == "following"){
        $('.follow-btn').hide();
    }
    else if(test1 == "not-following"){
        $('.following-btn').hide();
    }

    $('.follow-btn').on('click', function(){
        $(this).hide();
        $('.following-btn').show();
    });

    $('.following-btn').on('click', function(){
        $(this).next(".follow-btn").show();
        $(this).hide();
    });

    $('.following-btn').mouseover(function(){
        $(this).text("Dejar de seguir");
        $(this).css('background-color', 'rgb(202, 32, 85)');
    });

    $('.following-btn').mouseout(function(){
        $(this).text("Siguiendo");
        $(this).css('background-color', 'rgb(29, 161, 242)');
        
    });

    $('.tweet-button').on('click', function(){
        var tweetValue = $('#tweet-text').val();
        $.ajax({
            url: '/Start/NewTweet?content='+tweetValue,
            method: 'GET',
            success: function(response){
                $('#container-all-tweets').prepend(
                    '<div class="tweet-container">'+
                        '<img src="'+response.owner.profilePicture+'" class="dot-tweet">'+
                        '<div class="text-tweet-container">'+
                            '<div class="user-tweet-details">'+
                                '<p>'+response.owner.name+'</p>'+
                                '<p>@'+response.owner.username+'</p>'+
                                '<p>'+response.creationDay+' '+response.creationMonth+' '+response.creationYear+'</p>'+
                            '</div>'+
                            '<p>'+response.content+'</p>'+
                            '<div class="cm-rt-mg">'+
                                '<div class="comment-icon margin-right">'+
                                    '<a href="" ><img src="../img/icons/comment.png" alt=""></a>'+
                                    '<a href="" ><img src="../img/icons/comment-hover.png" alt="" class="hover-comment"></a>'+
                                    '<input type="hidden" id="tweetID" value="'+response.tweetID+'">'+
                                '</div>'+
                                '<div class="retweet-icon margin-right">'+
                                    '<a href=""><img src="../img/icons/retweet.png" alt=""></a>'+
                                    '<a href=""><img src="../img/icons/retweet-hover.png" alt="" class="hover-retweet"></a>'+
                                    '<input type="hidden" id="tweetID" value="'+response.tweetID+'">'+
                                '</div>'+
                                '<div class="like-icon">'+
                                    '<a href=""><img src="../img/icons/like.png" alt=""></a>'+
                                    '<a href=""><img src="../img/icons/like-hover.png" alt="" class="hover-like"></a>'+
                                    '<input type="hidden" id="tweetID" value="'+response.tweetID+'">'+
                                '</div>'+
                            '</div>'+
                        '</div>'+
                    '</div>'
                );
            },
            failure: function(error){
                console.log(error);
            }
        });
        $('#tweet-text').val("");
    });

    $('.hover-comment').on('click', function(){
        var tweetID = $(this).closest('div[class^=comment-icon').find('#tweetID').val();
        
        $.ajax({
            type: 'POST',
            url: '',
            data: {
                ID: tweetID
            }
        });
    });

    $('.follow-btn').on('click', function(){
        var identificator = $('#userID').val();
        
        $.ajax({
            type: 'POST',
            url: '/start/ToFollow',
            data: {
                ID: identificator
            }
        });
    });

    

    $('.following-btn').on('click', function(){
        var identificator = $('#userID').val();
        $.ajax({
            type: 'POST',
            url: '/start/Unfollow',
            data: {
                ID: identificator
            }
        });
    });

    $('#myBtn').on('click', function(){
        var userID = $('#userEmail').val();
        $.ajax({
            url: '/Start/EditProfile?mail='+userID,
            method: 'GET',
            success: function(response){
                $('#name-user').val(response.name);
                $('#biography-user').val(response.biography);
                $('#location-user').val(response.location);
            },
            failure: function(error){
                console.log(error);
            }
        });
        $('#myModal').css('display', 'block');
    });
  
    $('.close').on('click', function(){
        $('#myModal').css('display', 'none');
    });

    $('#comment-click').on('click', function(){
        $('#modal-new-comment').css('display', 'block');
    });
  
    $('.close').on('click', function(){
        $('#modal-new-comment').css('display', 'none');
    });

    $('.btn-reply').on('click', function(){
        $('#modal-new-comment').css('display', 'none');
        var commentContent = $('.reply-content').val();
        var tweetToReply = $('#tweet-content-to-reply').text();
        $.ajax({
            type: 'POST',
            url: '/start/AddComment',
            data: {
                comment: commentContent,
                tweetToReply: tweetToReply
            }
        });
    });

    $('.change-profile-picture').on('click', function(){
        $('#profile-url-container').css('display', 'flex');
        $('#myModal').css('display', 'none');
    });

    $('.close-url-container').on('click', function(){
        $('#profile-url-container').css('display', 'none');
        $('#myModal').css('display', 'block');
    });

    $('.save-url').on('click', function(){
        $('#profile-url-container').css('display', 'none');
        $('#myModal').css('display', 'block');
        var urlProfilePhoto = $('#input-url').val();

        $.ajax({
            type: 'POST',
            url: '/start/SaveProfilePicture',
            data: {
                URL: urlProfilePhoto
            }
        });
    });

    $('.change-cover-picture').on('click', function(){
        $('#cover-url-container').css('display', 'flex');
        $('#myModal').css('display', 'none');
    });
  
    $('.close-url-container').on('click', function(){
        $('#cover-url-container').css('display', 'none');
        $('#myModal').css('display', 'block');
    });

    $('#save-cover-profile').on('click', function(){
        $('#cover-url-container').css('display', 'none');
        $('#myModal').css('display', 'block');
        var urlCoverPhoto = $('#input-url-cover-picture').val();

        $.ajax({
            type: 'POST',
            url: '/start/SaveCoverPicture',
            data: {
                URL: urlCoverPhoto
            }
        });
    });

    

    $('body').on('click', '.fa-trash-alt', function(){
        var tweetID = $(this).next('#tweetID').val();
        $('.delete-container').css('display', 'flex');
        
        $('.delete-container').append(
            '<div class="delete-content">'+
                '<p class="warning-title">¿Quieres eliminar el Tweet?</p>'+
                '<p class="warning-text">Esta acción no se puede revertir, y se lo eliminará de tu perfil, de la cronología de las cuentas que te sigan y de los resultados de búsqueda de Twitter. </p>'+
                '<div class="delete-btns">'+
                '<span class="cancel-btn">Cancelar</span>+'+
                '<span class="confirm-btn">Eliminar</span>'+
                '<input type="hidden" class="tweetIdentificator" value="'+tweetID+'">'+
                '</div>'+
            '</div>'
        );
    });

    $('body').on('click', '.cancel-btn', function(){
        $('.delete-container').css('display', 'none');
    });


    $('body').on('click', '.confirm-btn', function(){
        var tweetIdentificator = $(this).next('.tweetIdentificator').val();
        
        $.ajax({
            type: 'POST',
            url: '/start/DeleteTweet',
            data: {
                ID: tweetIdentificator
            }
        });
        $('.delete-container').css('display', 'none');
        
    });
    
});