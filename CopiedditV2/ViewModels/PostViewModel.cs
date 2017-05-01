using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CopiedditV2.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        public int CommentsCount { get; set; }
    }
}
