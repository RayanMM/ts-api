using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TS_Security.Config
{
    public class SigningConfigurations
    {
        public SigningConfigurations()
        {
            using (RSACryptoServiceProvider provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }

        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }
    }
}
