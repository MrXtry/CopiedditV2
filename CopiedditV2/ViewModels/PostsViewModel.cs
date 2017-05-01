using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CopiedditV2.ViewModels
{
    public class PostsViewModel
    {
        public PostsViewModel()
        {
            Posts = new List<PostViewModel>();
        }

        public List<PostViewModel> Posts { get; set; }
    }
}