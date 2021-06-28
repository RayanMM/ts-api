using System;
using System.Collections.Generic;
using System.Text;
using TS.Domain.Entities;

namespace TS.Domain.Dtos
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public string UserMail { get; set; }
        public DateTime? RegisterDate { get; set; }

        public static UserDto From(User from)
        {
            return new UserDto
            {
                UserId = from.UserId,
                UserName = from.UserName,
                UserFullName = from.UserFullName,
                UserMail = from.UserMail,
                RegisterDate = from.RegisterDate
            };
        }
    }
}
