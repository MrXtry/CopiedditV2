using CopiedditV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopiedditV2.Repositories
{
    public interface ICommentRepository
    {
        Task<bool> CreateComment(CreateCommentViewModel model);
    }
}