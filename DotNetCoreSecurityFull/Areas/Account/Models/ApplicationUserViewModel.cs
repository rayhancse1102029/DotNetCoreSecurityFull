using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreSecurityFull.Data;
using Microsoft.AspNetCore.Identity;

namespace DotNetCoreSecurityFull.Areas.Account.Models
{
    public class ApplicationUserViewModel
    {
        public string email { get; set; }
        public string username { get; set; }
        public string fullName { get; set; }

        public List<IdentityUser> ApplicationUsers { get; set; }

    }
}
