using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using to_do_backend.Utils;

namespace to_do_backend.JWTConfiguration
{
    public class JwtAuth : IJwtAuth
    {
        private readonly BackendContext db;
        private readonly string key;
        public JwtAuth(string key, BackendContext db)
        {
            this.key = key;
            this.db = db;
        }

        public string Authentication(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password)) return null;

            User user = db.Users.FirstOrDefault(user => user.Username == username);
            if (user == null) return null;
            if (!Hasher.CheckPlaintextAgainstHash(password, user.Password, user.Salt)) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
