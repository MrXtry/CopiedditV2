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
            var posts = await _postRepository.GetAll();
            if (Request.QueryString.HasValue && Request.QueryString.Value == "?Vote")
                posts.Posts = posts.Posts.OrderByDescending(x => x.VoteCount).ToList();

            return View(posts);
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
