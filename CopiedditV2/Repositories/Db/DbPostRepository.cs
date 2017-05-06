﻿using CopiedditV2.Data;
using CopiedditV2.Models;
using CopiedditV2.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopiedditV2.Repositories.Db
{
    public class DbPostRepository : IPostRepository
    {
        private readonly CopiedditV2Context _context;

        public DbPostRepository(CopiedditV2Context context)
        {
            _context = context;
        }

        public async Task<bool> IdCheck(int? id)
        {
            try
            {
                if (id == null || !id.HasValue)
                    return false;
                else
                    return await _context.Posts.AnyAsync(p => p.ID == id);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<PostViewModel> Get(int id)
        {
            try
            {
                var post = await _context
                    .Posts
                    .Include(i => i.Comments)
                    .Where(x => x.ID == id)
                    .Select(p => new PostViewModel
                    {
                        Id = p.ID,
                        Title = p.Title,
                        DateCreated = p.DateCreated,
                        CommentsCount = 0,
                        Comments = p.Comments
                    })
                    .SingleOrDefaultAsync();

                //var test = await _context
                //    .Posts
                //    .Include(i => i.Comments)
                //    .Where(x => x.ID == id)
                //    .Select(p => new PostViewModel
                //    {
                //        Id = p.ID,
                //        Title = p.Title,
                //        DateCreated = p.DateCreated,
                //        CommentsCount = (p.Comments != null && p.Comments.Any()) ? _context.Comments.Where(c => c.PostID == p.ID).Count() : 0
                //    })
                //    .SingleOrDefaultAsync();

                return post;
            }
            catch (Exception ex)
            {
                return new PostViewModel();
            }
        }

        public async Task<bool> CreatePost(CreatePostViewModel model)
        {
            try
            {
                _context.Posts.Add(new Post
                {
                    Title = model.Title,
                    Url = "",
                    DateCreated = DateTime.Now
                });
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
