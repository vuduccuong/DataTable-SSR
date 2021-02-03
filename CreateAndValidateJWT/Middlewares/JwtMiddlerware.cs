using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using CreateAndValidateJWT.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Helpers;

namespace CreateAndValidateJWT.Middlewares
{
    public class JwtMiddlerware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public JwtMiddlerware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
            _config = config;
        }

        public async Task Invoke(HttpContext context, JwtDbContext dataContext)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                await AttachAccount(context, dataContext, token);
            }
            await _next(context);
        }

        private async Task AttachAccount(HttpContext context, JwtDbContext dataContext, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config.GetSection("JwtKey").Value);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //Set ClockKew = 0 để thông báo token đã hết hạn
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;

                int.TryParse(jwtToken.Claims.First(x=>x.Type=="id").Value, out int accountId);

                var id = await dataContext.Users.FindAsync(accountId);

                if (String.Equals(JWTHelpers.ValidateJwtToken(token,key),id.Id.ToString()))
                {
                    context.Items["Account"] = await dataContext.Users.FindAsync(accountId);
                }
            }
            catch
            {
                //do nothing
            }
        }
    }
}
