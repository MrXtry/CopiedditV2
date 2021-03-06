﻿using CopiedditV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopiedditV2.Repositories
{
    public interface IPostRepository
    {
        Task<bool> IdCheck(int? id);
        Task<PostViewModel> Get(int id);
        Task<PostsViewModel> GetAll();
        Task<bool> CreatePost(CreatePostViewModel model);
        Task<bool> UpdatePostVotePlus(UpdateVoteViewModel model);
        Task<bool> UpdatePostVoteMinus(UpdateVoteViewModel model);
    }
}
