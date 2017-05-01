using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CopiedditV2.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int ParentId { get; set; }

        public string Content { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
    }
}
