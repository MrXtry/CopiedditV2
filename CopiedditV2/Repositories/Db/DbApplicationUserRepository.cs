using CopiedditV2.Data;
using CopiedditV2.Models;
using CopiedditV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CopiedditV2.Repositories.Db
{
    public class DbApplicationUserRepository : IApplicationUserRepository
    {
        private readonly CopiedditV2Context _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public DbApplicationUserRepository(CopiedditV2Context context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManger;
        }

        public async Task<bool> CreateAppUser(RegisterViewModel model)
        {
            try
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                //_context.Authors.Add(new Author
                //{
                //    Email = model.Email,
                //    Password = model.Password,
                //    DateCreated = DateTime.Now
                //});
                //await _context.SaveChangesAsync();

                return result.Succeeded;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ApplicationUser> GetUser(string email)
        {
            try
            {
                var user = await _context.ApplicationUsers.SingleOrDefaultAsync(a => a.Email == email);
                return user;
            }
            catch (Exception ex)
            {
                return new ApplicationUser();
            }
        }
    }
}
