using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Suls.Data
{
    public class Problem
    {
        //•	Has a Name – a string with min length 5 and max length 20 (required)
        //•	Has Points– an integer between 50 and 300 (required)

        public Problem()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Submissions = new HashSet<Submission>();
        }
        public string Id { get; set; }
        
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public ushort Points { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
