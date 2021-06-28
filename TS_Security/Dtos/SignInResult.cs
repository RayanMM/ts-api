using System;
using System.Collections.Generic;
using System.Text;

namespace TS_Security.Dtos
{
    public class SignInResult
    {
        public bool Success { get; set; }
        public string TokenType { get; set; }
        public string AccessToken { get; set; }
    }
}
