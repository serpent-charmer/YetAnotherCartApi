using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using YetAnotherCartApi.Auth.Context;
using YetAnotherCartApi.Auth.Dto;
using YetAnotherCartApi.Auth.Entity;

namespace YetAnotherCartApi.Auth.Controllers
{
    public class AuthController : Controller
    {
        private UserContext authContext;
        public AuthController(UserContext authContext) { 
            this.authContext = authContext;
        }

        [HttpPost]
        [Route("/api/[action]")]
        public async Task<IActionResult> Register([FromBody] UserRegisterInfo user)
        {
            if (string.IsNullOrEmpty(user.Username))
                return BadRequest("username is required");
            var usr = await authContext.Users.FirstOrDefaultAsync(u => u.Username == user.Username);
            if (usr != null)
            {
                return BadRequest("user already exists");
            }
            await authContext.Users.AddAsync(new ShopUser{ 
                Username = user.Username
            });
            await authContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        [Route("/api/[action]")]
        public async Task<IActionResult> Login([FromBody] string? username)
        {
            if(string.IsNullOrEmpty(username))
                return BadRequest("username is required");
            var usr = await authContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (usr == null)
            {
                return BadRequest("you should register first");
            }
            var jwt = new ShopAuthorization().Login(usr.Uid.ToString());
            return Ok(jwt);
        }

    }
}
