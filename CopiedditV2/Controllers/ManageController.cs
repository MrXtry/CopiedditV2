using CopiedditV2.Data;
using CopiedditV2.Models;
using CopiedditV2.Models.ManageViewModels;
using CopiedditV2.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CopiedditV2.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly CopiedditV2Context _context;
        IApplicationUserRepository _applicationUserRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly string _externalCookieScheme;
        private readonly ILogger _logger;

        public ManageController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManger,
            IOptions<IdentityCookieOptions> identityCookieOptions,
            ILoggerFactory loggerFactory,
            CopiedditV2Context context,
            IApplicationUserRepository applicationUserRepository)
        {
            _userManager = userManager;
            _signInManager = signInManger;
            _externalCookieScheme = identityCookieOptions.Value.ExternalCookieAuthenticationScheme;
            _logger = loggerFactory.CreateLogger<ManageController>();
            _context = context;
            _applicationUserRepository = applicationUserRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
                return View("Error");

            var getUser = await _applicationUserRepository.GetUser(user.Email);

            var model = new IndexViewModel
            {
                HasPassword = await _userManager.HasPasswordAsync(user),
                Logins = await _userManager.GetLoginsAsync(user),
                BrowserRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user),
                Image = getUser.Image
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await GetCurrentUserAsync();
            if (user != null)
            {
                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User changed their password successfully.");
                    return RedirectToAction(nameof(Index), new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                AddErrors(result);
                return View(model);
            }
            return RedirectToAction(nameof(Index), new { Message = ManageMessageId.Error });
        }
        
        [HttpPost]
        public async Task<IActionResult> UploadImage(IList<IFormFile> files)
        {
            var user = await GetCurrentUserAsync();
            IFormFile uploadedImage = files.FirstOrDefault();
            if ((uploadedImage == null || uploadedImage.ContentType.ToLower().StartsWith("image/")) && user != null)
            {
                using (_context)
                {
                    MemoryStream ms = new MemoryStream();
                    uploadedImage.OpenReadStream().CopyTo(ms);

                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                    Models.Image imageEntity = new Models.Image()
                    {
                        Name = uploadedImage.Name,
                        Data = ms.ToArray(),
                        Width = image.Width,
                        Height = image.Height,
                        ContentType = uploadedImage.ContentType,
                        ApplicationUserId = user.Id
                    };

                    _context.Images.Add(imageEntity);

                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public FileStreamResult ViewImage(int id)
        {
            using (_context)
            {
                Image image = _context.Images.FirstOrDefault(i => i.Id == id);
                MemoryStream ms = new MemoryStream(image.Data);

                return new FileStreamResult(ms, image.ContentType);
            }
        }


        #region Helpers 
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            AddLoginSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
        #endregion
    }
}
