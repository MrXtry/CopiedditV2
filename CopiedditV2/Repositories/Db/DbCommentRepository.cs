using CopiedditV2.Data;
using CopiedditV2.Models;
using CopiedditV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopiedditV2.Repositories.Db
{
    public class DbCommentRepository : ICommentRepository
    {
        private readonly CopiedditV2Context _context;

        public DbCommentRepository(CopiedditV2Context context)
        {
            _context = context;
        }

        public async Task<bool> CreateComment(CreateCommentViewModel model)
        {
            try
            {
                _context.Comments.Add(new Comment
                {
                    PostID = model.PostId,
                    Content = model.Content,
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
