using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CopiedditV2.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        //[ForeignKey("Author")]
        //public int AuthorID { get; set; }
        public string ApplicationUserId { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }
        
        public int? ParentId { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }
        //[ForeignKey("AuthorID")]
        //public Author Author { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
