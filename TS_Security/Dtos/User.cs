using System;
using System.Collections.Generic;
using System.Text;

namespace TS_Security.Dtos
{
    public class UserSecurity
    {
        public long UserId { get; set; }
        public string Username { get; set; }
        public string UserFullName { get; set; }
        public int UserLevel { get; set; }
        public bool IsEnabled { get; set; }
    }
}
