using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterClone.Models
{
    public class Tweet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TweetID { get; set; }
        public string Content { get; set; }
        public string UserCreator { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime Date { get; set; }
        public int Like { get; set; }
        public int Retweet { get; set; }
    }
}