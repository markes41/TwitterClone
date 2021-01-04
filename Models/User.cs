using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterClone.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Mail { get ; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Year { get; set; }
        public List<Tweet> Tweets { get; set; }
        public List<User> Seguidores { get; set; }
        public List<User> Seguidos { get; set; }
    }
}