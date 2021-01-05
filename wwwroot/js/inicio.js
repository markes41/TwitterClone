$(document).ready(function(){
    $.ajax({
            url: '/Start/BringTweets',
            method: 'GET',
            success: function(response){
                $('#container-all-tweets').empty();
                for(var i=0;i<response.length;i++){
                    $('#container-all-tweets').append(
                        '<div class="tweet-container">'+
                            '<span class="dot-tweet"></span>'+
                            '<div class="text-tweet-container">'+
                                '<div class="user-tweet-details">'+
                                    '<p>Mike Luca</p>'+
                                    '<p>@'+response[i].owner.username+'</p>'+
                                    '<p>15:25</p>'+
                                '</div>'+
                                '<p>'+response[i].content+'</p>'+
                                '<div class="cm-rt-mg">'+
                                    '<div class="comment-icon">'+
                                        '<a href="" ><img src="../img/icons/comment.png" alt=""></a>'+
                                        '<a href="" ><img src="../img/icons/comment-hover.png" alt="" class="hover-comment"></a>'+
                                        '<input type="hidden" id="tweetID" value="'+response[i].tweetID+'">'+
                                    '</div>'+
                                    '<div class="retweet-icon">'+
                                        '<a href=""><img src="../img/icons/retweet.png" alt=""></a>'+
                                        '<a href=""><img src="../img/icons/retweet-hover.png" alt="" class="hover-retweet"></a>'+
                                        '<input type="hidden" id="tweetID" value="'+response[i].tweetID+'">'+
                                    '</div>'+
                                    '<div class="like-icon">'+
                                        '<a href=""><img src="../img/icons/like.png" alt=""></a>'+
                                        '<a href=""><img src="../img/icons/like-hover.png" alt="" class="hover-like"></a>'+
                                        '<input type="hidden" id="tweetID" value="'+response[i].tweetID+'">'+
                                    '</div>'+
                                '</div>'+
                            '</div>'+
                        '</div>'
                    );                    
                }
            },
            failure: function(error){
                console.log("Algo ha salido mal: "+error);
            }
    });
});