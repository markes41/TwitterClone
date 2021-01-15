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
                        '<span class="dot-tweet"></span>'+
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
});