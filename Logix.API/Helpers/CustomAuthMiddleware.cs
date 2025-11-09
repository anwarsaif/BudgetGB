using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Logix.API.Helpers
{
    public class CustomAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Validate the token
            if (context.User.Identity.IsAuthenticated)
            {
                // Get the user ID or other relevant information from the token claims
                string userId = context.User.FindFirst("UserId")?.Value;

                // Create a session for the user
                context.Session.SetString("UserId", userId);

                // Other session data can be stored as needed
                // context.Session.SetInt32("SomeKey", someValue);
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
