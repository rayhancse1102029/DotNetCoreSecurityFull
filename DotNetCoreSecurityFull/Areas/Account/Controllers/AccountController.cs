using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreSecurityFull.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreSecurityFull.Areas.Account.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly ApplicationDbContext context;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.context = context;

        }

        [HttpPost]
        public async Task<IActionResult> PasswordResetByAdmin(string userName)
        {

            #region Find User

            // another way
            //var user = await context.Users.FindAsync(userName);

            //var userInfo = context.Users.FirstOrDefault(u => u.UserName == userName);

            IdentityUser identityUser = await userManager.FindByNameAsync(userName);

            #endregion

           if(identityUser.PasswordHash == null)
            {
                // password already removed
            }
            else
            {
                await userManager.RemovePasswordAsync(identityUser);

            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> PasswordSetByAdmin(string userName, string newPassword)
        {

            IdentityUser identityUser = await userManager.FindByNameAsync(userName);

            if (identityUser.PasswordHash == null)
            {
                await userManager.AddPasswordAsync(identityUser, newPassword);
            }
            else
            {
               // user already have a password
            }
            return RedirectToAction("Index");

        }
    }
}