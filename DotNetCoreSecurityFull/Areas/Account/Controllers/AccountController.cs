using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreSecurityFull.Areas.Account.Models;
using DotNetCoreSecurityFull.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DotNetCoreSecurityFull.Models;

namespace DotNetCoreSecurityFull.Areas.Account.Controllers
{
    [Area("Account")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ApplicationDbContext context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            ApplicationUserViewModel model = new ApplicationUserViewModel
            {
               ApplicationUsers  = await context.Users.ToListAsync()
            };
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PasswordResetByAdmin(string userName)
        {
            #region Find User

            // another way
            //var user = await context.Users.FindAsync(userName);

            //var userInfo = context.Users.FirstOrDefault(u => u.UserName == userName);

            ApplicationUser applicationUser = await userManager.FindByNameAsync(userName);

            #endregion

           if(applicationUser.PasswordHash == null)
            {
                // password already removed
            }
            else
            {
                await userManager.RemovePasswordAsync(applicationUser);

            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> PasswordSetByAdmin(string userName, string newPassword)
        {

            ApplicationUser applicationUser = await userManager.FindByNameAsync(userName);

            if (applicationUser.PasswordHash == null)
            {
                await userManager.AddPasswordAsync(applicationUser, newPassword);
            }
            else
            {
               // user already have a password
            }
            return RedirectToAction("Index");

        }
    }
}