using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Suls.Data
{
    public class User
    {
        //•	Has a Username – a string with min length 5 and max length 20 (required)
        //•	Has an Email - a string, which holds only valid email(required)
        //•	Has a Password – a string with min length 6 and max length 20  - hashed in the database(required)
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Submissions = new HashSet<Submission>();
        }

        public string Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
