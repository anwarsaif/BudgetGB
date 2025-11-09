using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Infrastructure.Services
{
    public class CurrentUserService: ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        public void GetUserData()
        {
            var user = httpContextAccessor.HttpContext.User;

            if (user.Identity.IsAuthenticated)
            {
                // The user is authenticated
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                Console.WriteLine($"%%%%%%% CurrentUser : {userId}");
                // Do something with the user ID
            }
            else
            {
                Console.WriteLine($"%%%%%%% Not Authenticated : {user}");
            }
        }
    }
}
