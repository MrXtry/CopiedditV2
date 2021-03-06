﻿using CopiedditV2.Models;
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

        public int VoteCount { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        public int CommentsCount { get; set; }

        public List<CommentViewModel> Comments { get; set; }
    }
}
