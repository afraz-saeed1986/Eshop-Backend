using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Utilities.Extensions.Identity
{
    public static class IdentityUserExtension
    {
        public static long GetuserId(this ClaimsPrincipal claimsPrincipal)
        {
            if(claimsPrincipal != null)
            {
                var result = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
                return Convert.ToInt64(result.Value);
            }

            return default(long);
        }
    }
}
