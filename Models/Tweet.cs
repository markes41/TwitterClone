using System;
using System.Collections.Generic;
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
        public User Owner { get; set; }
        public int Comments { get; set; }
    }
}