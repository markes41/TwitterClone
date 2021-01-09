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

    var test1 = $('#test').val();

    if(test1 == "following"){
        $('#unfollow-button').hide();
        $('#follow-button').hide();
    }
    else if(test1 == "not-following"){
        $('#unfollow-button').hide();
        $('#following-button').hide();
    }

    $('#follow-button').on('click', function(){
        $('#follow-button').hide();
        $('#following-button').show();
    });

    $('#following-button').on('click', function(){
        $('#follow-button').show();
        $('#following-button').hide();
    });

    $('#following-button').mouseover(function(){
        $('#following-button').text("Dejar de seguir");
        $('#following-button').css('background-color', 'rgb(202, 32, 85)');
    });

    $('#following-button').mouseout(function(){
        $('#following-button').text("Siguiendo");
        $('#following-button').css('background-color', 'rgb(29, 161, 242)');
        
    });

    

    $('.tweet-button').on('click', function(){
        var tweetValue = $('#tweet-text').val();
        $.ajax({
            type: 'POST',
            url: '/start/NewTweet',
            data: {
                content: tweetValue
            }
        });

        $('#tweet-text').val("");

        
    });

    $('.hover-retweet').on('click', function(){
        var tweetID = $(this).closest('div[class^=retweet-icon').find('#tweetID').val();
        
        $.ajax({
            type: 'POST',
            url: '/start/ToRetweet',
            data: {
                ID: tweetID
            }
        });
    });

    $('.hover-like').on('click', function(){
        var tweetID = $(this).closest('div[class^=like-icon').find('#tweetID').val();
        
        $.ajax({
            type: 'POST',
            url: '/start/ToRetweet',
            data: {
                ID: tweetID
            }
        });
    });

    $('.hover-comment').on('click', function(){
        var tweetID = $(this).closest('div[class^=comment-icon').find('#tweetID').val();
        
        $.ajax({
            type: 'POST',
            url: '/start/ToRetweet',
            data: {
                ID: tweetID
            }
        });
    });

    $('#follow-button').on('click', function(){
        var identificator = $('#userID').val();
        
        $.ajax({
            type: 'POST',
            url: '/start/ToFollow',
            data: {
                ID: identificator
            }
        });
    });

    

    $('#following-button').on('click', function(){
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
        $.ajax({
        url: '/start/EditProfile',
        method: 'GET',
        success: function(response){
            $('#name-user').val(response.name);
            $('#location-user').val(response.location);
            $('#biography-user').val(response.biography);
        },
        failure: function(error){
            console.log(error);
        }
        });
    });


});