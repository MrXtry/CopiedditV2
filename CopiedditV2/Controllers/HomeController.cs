using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CopiedditV2.Data;
using Microsoft.EntityFrameworkCore;
using CopiedditV2.ViewModels;
using CopiedditV2.Models;
using CopiedditV2.Repositories;

namespace CopiedditV2.Controllers
{
    public class HomeController : Controller
    {
        private readonly CopiedditV2Context _context;
        IPostRepository _postRepository;

        public HomeController(CopiedditV2Context context, IPostRepository postRepository)
        {
            _context = context;
            _postRepository = postRepository;
        }

        public async Task<ActionResult> Index()
        {
            //var viewModel = new PostsViewModel();
            //var posts = await _context.Posts
            //    .Include(i => i.Comments)
            //    .ToListAsync();

            //foreach (var post in posts)
            //{
            //    viewModel.Posts.Add(new PostViewModel
            //    {
            //        Id = post.ID,
            //        Title = post.Title,
            //        DateCreated = post.DateCreated,
            //        CommentsCount = (post.Comments != null && post.Comments.Any()) ? _context.Comments.Where(c => c.PostID == post.ID).Count() : 0
            //    });
            //}


            //return View(viewModel.Posts.ToList());

            return View(await _postRepository.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVotePlus(UpdateVoteViewModel updateVoteViewModel)
        {
            if (!await _postRepository.IdCheck(updateVoteViewModel.PostId))
                return NoContent();
            if (!await _postRepository.UpdatePostVotePlus(updateVoteViewModel))
                return NoContent();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVoteMinus(UpdateVoteViewModel updateVoteViewModel)
        {
            if (!await _postRepository.IdCheck(updateVoteViewModel.PostId))
                return NoContent();
            if (!await _postRepository.UpdatePostVoteMinus(updateVoteViewModel))
                return NoContent();

            return RedirectToAction("Index");
        }
    }
}
