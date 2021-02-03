using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace Helpers
{
    public static class JWTHelpers
    {
        /// <summary>
        /// Tạo token
        /// </summary>
        /// <param name="accountId">accountID</param>
        /// <param name="key">key get từ appsetting.json</param>
        /// <returns></returns>
        public static string GenerationToken(int accountId, byte[] key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", accountId.ToString())
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    
        public static int? ValidateJwtToken(string token, byte[] key)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
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
                int.TryParse(jwtToken.Claims.First(x => x.Type == "id").Value,out int accountID);
                return accountID;
            }
            catch
            {

                //return null if validation fails
                return null;
            }
        }
    }
}
