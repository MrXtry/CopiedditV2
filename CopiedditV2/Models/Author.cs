using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopiedditV2.Models
{
    public class Author
    {
        public int ID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime DateCreated { get; set; }

        public ICollection<Post> Posts { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}