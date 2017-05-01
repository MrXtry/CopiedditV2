using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CopiedditV2.Data;
using CopiedditV2.ViewModels;
using CopiedditV2.Repositories;

namespace CopiedditV2.Controllers
{
    public class PostController : Controller
    {
        IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: Post
        public async Task<ActionResult> Index(int? id)
        {
            if (!await _postRepository.IdCheck(id))
                return NotFound();

            var test = await _postRepository.Get(id.Value);

            return View(await _postRepository.Get(id.Value));
        }

        //// GET: Post/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var postViewModel = await _context.PostViewModel
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (postViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(postViewModel);
        //}

        // GET: Post/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Post/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,DateCreated,CommentsCount")] PostViewModel postViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(postViewModel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    return View(postViewModel);
        //}

        //// GET: Post/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var postViewModel = await _context.PostViewModel.SingleOrDefaultAsync(m => m.Id == id);
        //    if (postViewModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(postViewModel);
        //}

        //// POST: Post/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Title,DateCreated,CommentsCount")] PostViewModel postViewModel)
        //{
        //    if (id != postViewModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(postViewModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PostViewModelExists(postViewModel.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View(postViewModel);
        //}

        //// GET: Post/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var postViewModel = await _context.PostViewModel
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (postViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(postViewModel);
        //}

        //// POST: Post/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var postViewModel = await _context.PostViewModel.SingleOrDefaultAsync(m => m.Id == id);
        //    _context.PostViewModel.Remove(postViewModel);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //private bool PostViewModelExists(int id)
        //{
        //    return _context.PostViewModel.Any(e => e.Id == id);
        //}
    }
}
