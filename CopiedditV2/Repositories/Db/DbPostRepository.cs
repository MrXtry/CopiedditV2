using CopiedditV2.Data;
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
                    return await _context.Posts.AnyAsync(p => p.Id == id);
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
                    .Where(x => x.Id == id)
                    .Select(p => new PostViewModel
                    {
                        Id = p.Id,
                        Title = p.Title,
                        DateCreated = p.DateCreated,
                        CommentsCount = 0,
                        Comments = p.Comments.Select(c => new CommentViewModel
                        {
                            Id = c.Id,
                            PostId = c.PostId,
                            ParentId = (c.ParentId.HasValue) ? c.ParentId.Value : 0,
                            Content = c.Content,
                            DateCreated = c.DateCreated,
                        })
                        .ToList()
                    })
                    .SingleOrDefaultAsync();

                post.CommentsCount = post.Comments.Count();

                var sections = post.Comments.OrderBy(x => x.ParentId).ToList();
                var stack = new Stack<CommentViewModel>();

                foreach (var section in sections.Where(x => x.ParentId == default(int)).Reverse())
                {
                    stack.Push(section);
                    sections.RemoveAt(0);
                }

                var output = new List<CommentViewModel>();
                while (stack.Any())
                {
                    var currentSection = stack.Pop();
                    var children = sections.Where(x => x.ParentId == currentSection.Id).Reverse();

                    foreach (var section in children)
                    {
                        stack.Push(section);
                        sections.Remove(section);
                    }
                    output.Add(currentSection);
                }
                post.Comments = output;

                return post;
            }
            catch (Exception ex)
            {
                return new PostViewModel();
            }
        }

        public async Task<PostsViewModel> GetAll()
        {
            try
            {
                var viewModel = new PostsViewModel();
                var posts = await _context.Posts
                    .Include(i => i.Comments)
                    .ToListAsync();

                foreach (var post in posts)
                {
                    viewModel.Posts.Add(new PostViewModel
                    {
                        Id = post.Id,
                        Title = post.Title,
                        VoteCount = post.VoteCount,
                        DateCreated = post.DateCreated,
                        CommentsCount = (post.Comments != null && post.Comments.Any()) ? _context.Comments.Where(c => c.PostId == post.Id).Count() : 0
                    });
                }

                return viewModel;
            }
            catch (Exception ex)
            {
                return new PostsViewModel();
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

        public async Task<bool> UpdatePostVotePlus(UpdateVoteViewModel model)
        {
            try
            {
                var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == model.PostId);
                post.VoteCount++;

                _context.Posts.Update(post);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdatePostVoteMinus(UpdateVoteViewModel model)
        {
            try
            {
                var post = await _context.Posts.FirstOrDefaultAsync(x => x.Id == model.PostId);
                post.VoteCount--;

                _context.Posts.Update(post);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
