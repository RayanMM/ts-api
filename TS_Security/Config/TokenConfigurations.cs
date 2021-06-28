using System;
using System.Collections.Generic;
using System.Text;

namespace TS_Security.Config
{
    public class TokenConfigurations
    {
        public string Audience { get; set; }
        public int Hours { get; set; }
        public string Issuer { get; set; }
    }
}
