using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Readible.Requests
{
    public class UpdateUserRequest
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
