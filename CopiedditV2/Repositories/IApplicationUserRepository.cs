using CopiedditV2.Models;
using CopiedditV2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopiedditV2.Repositories
{
    public interface IApplicationUserRepository
    {
        Task<bool> CreateAppUser(RegisterViewModel model);
        Task<ApplicationUser> GetUser(string email);
    }
}