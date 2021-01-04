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

    $('.tweet-button').on('click', function(){
        var tweetValue = $('#tweet-text').val();
        $.ajax({
            type: 'POST',
            url: '/start/NewTweet',
            data: {
                content: tweetValue
            },
            success: function (response) {
                console.log(response);
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


});