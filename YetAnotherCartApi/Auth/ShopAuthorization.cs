using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YetAnotherCartApi.Auth.Context;
using YetAnotherCartApi.Auth.Entity;

namespace YetAnotherCartApi.Auth
{
    public class ShopAuthorization
    {
        private const string ISSUER = "shop";
        private const string AUDIENCE = "customers";
        private const string SECRET_KEY = "BgEh54ADGQ3TUWg9DD4JYprGZfuF1djI";
        private static SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
        private const string TOKEN_NAME = "shop_token_id";

        public ShopAuthorization() { 
        }

        public void Init(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = ISSUER,
                    ValidateAudience = true,
                    ValidAudience = AUDIENCE,
                    ValidateLifetime = true,
                    IssuerSigningKey = key,
                    ValidateIssuerSigningKey = true
                };
            });
        }


        public string Login(string username)
        {
            var claims = new List<Claim> { new Claim(TOKEN_NAME, username) };
            var jwt = new JwtSecurityToken(
                    issuer: ISSUER,
                    audience: AUDIENCE,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(2)),
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string? GetIdFromClaims(IEnumerable<Claim> claim)
        {
            return claim.FirstOrDefault(c => c.Type == TOKEN_NAME)?.Value;
        }

        public ShopUser? GetUser(IEnumerable<Claim> claim, DbSet<ShopUser> users)
        {
            var userId = GetIdFromClaims(claim);
            var usr = users.FirstOrDefault(u => u.Uid == Guid.Parse(userId));
            return usr;
        }
    }
}
