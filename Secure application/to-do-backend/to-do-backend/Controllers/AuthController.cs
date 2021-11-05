using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using to_do_backend.Dtos;
using to_do_backend.JWTConfiguration;

namespace to_do_backend.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BackendContext db;
        private readonly IJwtAuth jwtAuth;
        public AuthController(BackendContext backendContext, IJwtAuth auth)
        {
            db = backendContext;
            jwtAuth = auth;
        }

        [HttpPost]
        public ActionResult<string> authentication([FromBody] LoginRequestDto requestDto)
        {
            var token = jwtAuth.Authentication(requestDto.username, requestDto.password);
            if (token == null) return Unauthorized();
            return Ok(token);
        }

    }
}
