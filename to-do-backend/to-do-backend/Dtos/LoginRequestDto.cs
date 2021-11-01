using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace to_do_backend.Dtos
{
    public class LoginRequestDto
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
