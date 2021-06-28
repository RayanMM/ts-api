using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TS_Api
{
    public class UserNotFoundDetails : ProblemDetails
    {
        public UserNotFoundDetails()
        {
            Type = "not_found_exception";
            Title = "Not found";
            Detail = "the user or password is incorrect";
        }
    }
}
