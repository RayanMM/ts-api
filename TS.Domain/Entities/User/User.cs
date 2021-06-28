using Dapper.Contrib.Extensions;
using System;

namespace TS.Domain.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string UserPass { get; set; }
        public string UserMail { get; set; }
        public int UserSexo { get; set; }
        public int UserLevel { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string userToken { get; set; }
    }
}
