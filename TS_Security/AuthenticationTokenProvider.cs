using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using TS_Security.Config;
using TS_Security.Dtos;

namespace TS_Security
{
    public class AuthenticationTokenProvider
    {
        private readonly IConfiguration configuration;
        private readonly TokenConfigurations tokenConfigurations;

        public AuthenticationTokenProvider(IConfiguration configuration, TokenConfigurations tokenConfigurations)
        {
            this.configuration = configuration;
            this.tokenConfigurations = tokenConfigurations;
        }

        public virtual AuthWebToken CreateToken(UserSecurity userLogged)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimsConstant.UserId, userLogged.UserId.ToString()),
                new Claim(ClaimsConstant.Login, userLogged.Username),
                new Claim(ClaimsConstant.Name, userLogged.UserFullName)
            };

            DateTime dateCreate = DateTime.Now;

            DateTime dateExpires = dateCreate + TimeSpan.FromHours(tokenConfigurations.Hours);

            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(userLogged.UserId.ToString(), "Token"), claims);

            string secretKey = configuration["auth:AUTH_SECRETKEY"];

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            SigningCredentials signingCredentials= new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            JwtSecurityToken token = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Audience = tokenConfigurations.Audience,
                Issuer = tokenConfigurations.Issuer,
                SigningCredentials = signingCredentials,
                NotBefore = dateCreate,
                Expires = dateExpires,
                Subject = identity
            });

            return new AuthWebToken(tokenHandler.WriteToken(token), dateExpires);
        }

        public string DecriptToken(string authToken)
        {
            try
            {
                var key = Encoding.ASCII.GetBytes(configuration["auth:AUTH_SECRETKEY"]);
                var handler = new JwtSecurityTokenHandler();
                var validations = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                var claims = handler.ValidateToken(authToken, validations, out var tokenSecure);

                return claims.Identity.Name;
            }
            catch
            {
                return null;
            }
        }
    }

}
