using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopiedditV2.Models
{
    public class Post
    {
        public int Id { get; set; }

        //public int AuthorID { get; set; }
        public string ApplicationUserId { get; set; }

        [StringLength(50)]
        public string Title { get; set; }
        
        public string Url { get; set; }

        public int VoteCount { get; set; }

        public DateTime DateCreated { get; set; }

        public ICollection<Comment> Comments { get; set; }

        //public Author Author { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
