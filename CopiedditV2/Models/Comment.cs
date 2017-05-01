using System;

namespace CopiedditV2.Models
{
    public class Comment
    {
        public int ID { get; set; }

        public int PostID { get; set; }
        
        public int? ParentID { get; set; }

        public string Content { get; set; }

        public DateTime DateCreated { get; set; }

        public Post Post { get; set; }
    }
}
