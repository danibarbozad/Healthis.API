using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Healthis.API.Authentication
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext() 
            : base("HealthisAuth")
        {

        }
    }
}