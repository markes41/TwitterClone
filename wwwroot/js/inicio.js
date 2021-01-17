$(document).ready(function(){
    $.ajax({
            url: '/Start/BringTweets',
            method: 'GET',
            success: function(response){
                $('#container-all-tweets').empty();
                for(var i=0;i<response.length;i++){
                    $('#container-all-tweets').append(
                            '<div class="tweet-container">'+
                                '<a href="/start/search?username='+response[i].owner.username+'" alt="" style="height: 4rem;"><img src="'+response[i].owner.profilePicture+'" class="dot-tweet"></a>'+
                                '<div class="text-tweet-container">'+
                                    '<div class="user-tweet-details">'+
                                        '<p>'+response[i].owner.name+'</p>'+
                                        '<p>@'+response[i].owner.username+'</p>'+
                                        '<p>'+response[i].creationDay+' '+response[i].creationMonth+' '+response[i].creationYear+'</p>'+
                                    '</div>'+
                                    '<p>'+response[i].content+'</p>'+
                                    '<div class="cm-rt-mg">'+
                                        '<div class="comment-icon margin-right">'+
                                            '<a href="/start/tweet?tweetid='+response[i].tweetID+'" ><img src="../img/icons/comment.png" alt=""></a>'+
                                            '<a href="/start/tweet?tweetid='+response[i].tweetID+'" ><img src="../img/icons/comment-hover.png" alt="" class="hover-comment"></a>'+
                                            '<input type="hidden" id="tweetID" value="'+response[i].tweetID+'">'+
                                        '</div>'+
                                        '<div class="retweet-icon margin-right">'+
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