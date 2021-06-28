using System;
using System.Collections.Generic;
using System.Text;

namespace TS_Security.Dtos
{
    public class AuthWebToken
    {
        public AuthWebToken(string accessToken, DateTime expiresIn)
        {
            AccessToken = accessToken;
            TokenType = "bearer ";
            TokenCreate = DateTime.Now;
            ExpiresIn = expiresIn;
        }

        protected AuthWebToken()
        {

        }

        public string AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
        public DateTime TokenCreate { get; set; }
        public string TokenType { get; set; }
    }
}
