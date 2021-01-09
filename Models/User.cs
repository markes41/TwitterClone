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
        [Required]
        public string Name { get; set; }
        public string Month { get; set; }
        public string Day { get; set; }
        public string Year { get; set; }
        public string Location { get; set; }
        public string Biography { get; set; }
        public string CreationDay { get; set; }
        public string CreationMonth { get; set; }
        public string CreationYear { get; set; }
        public List<Tweet> Tweets { get; set; }
        public List<User> Followers { get; set; }
        public List<User> Following { get; set; }
    }
}