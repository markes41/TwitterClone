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
            }
        });

        /*$('.new-tweet').append(
            '<div class="tweet-container">'+
                '<span class="dot-tweet"></span>'+
                '<div class="text-tweet-container">'+
                    '<div class="user-tweet-details">'+
                        '<p>Mike Luca</p>'+
                        '<p>@tweets.Owner.Username</p>'+
                        '<p>15:25</p>'+
                    '</div>'+
                    '<p>'+tweetValue+'</p>'+
                    '<div class="cm-rt-mg">'+
                        '<div class="comment-icon">'+
                            '<a href="" ><img src="~/img/icons/comment.png" alt=""></a>'+
                            '<a href="" ><img src="~/img/icons/comment-hover.png" alt="" class="hover-comment"></a>'+
                            '<input type="hidden" id="tweetID" value="@tweets.TweetID">'+
                        '</div>'+
                        '<div class="retweet-icon">'+
                            '<a href=""><img src="~/img/icons/retweet.png" alt=""></a>'+
                            '<a href=""><img src="~/img/icons/retweet-hover.png" alt="" class="hover-retweet"></a>'+
                            '<input type="hidden" id="tweetID" value="@tweets.TweetID">'+
                        '</div>'+
                        '<div class="like-icon">'+
                            '<a href=""><img src="~/img/icons/like.png" alt=""></a>'+
                            '<a href=""><img src="~/img/icons/like-hover.png" alt="" class="hover-like"></a>'+
                            '<input type="hidden" id="tweetID" value="@tweets.TweetID">'+
                        '</div>'+
                    '</div>'+
                '</div>'+
                
            '</div>'
            );*/

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