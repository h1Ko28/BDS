using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Dto
{
    public class LoginReqDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}