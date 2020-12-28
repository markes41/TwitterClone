using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterClone.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Contact { get ; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Year { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public List<Tweet> Tweets { get; set; }
    }
}