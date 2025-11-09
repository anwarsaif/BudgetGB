using Logix.API.ViewModels;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices.Main;
using Logix.Application.Wrapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;


namespace Logix.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IResult<AuthModel>> Login(LoginDto login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return await Result<AuthModel>.FailAsync($"يجب ادخال البيانات بشكل صحيح");
                }

                var res = await authService.Login(login);
                var auth = new AuthModel();
               // return auth;
                return await Result<AuthModel>.SuccessAsync(auth, $"");
            }
            catch (Exception ex)
            {
                return await Result<AuthModel>.FailAsync($"======= Exp in login: {ex.Message}");
            }
        }      

        private bool IsOnlyNumbers(string input)
        {
            return Regex.IsMatch(input, @"^[0-9]+$");
        }

        private bool IsComplexPassword(string password)
        {
            bool containsNumbers = Regex.IsMatch(password, @"\d");
            bool containsChars = Regex.IsMatch(password, @"[a-zA-Z]");

            return containsNumbers && containsChars;
        }

    }
}
