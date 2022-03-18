using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;
using RegistrationLoginApi.Helpers;
using RegistrationLoginApi.Services;
using DevConsulting.RegistrationLoginApi.Client;

namespace RegistrationLoginApi.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);
            if (userId != null)
            {
                // create a session with the userId on successful auth
                context.Session.SetString("userid", userService.GetById(userId.Value).Id.ToString());
            }

            await _next(context);
        }
    }
}