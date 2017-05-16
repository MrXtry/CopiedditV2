using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopiedditV2.ViewModels
{
    public class CreateCommentViewModel
    {
        public string Content { get; set; }

        public int PostId { get; set; }

        public int ParentId { get; set; }
    }
}
