using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace to_do_backend.JWTConfiguration
{
    public interface IJwtAuth
    {
        string Authentication(string username, string password);
    }
}
