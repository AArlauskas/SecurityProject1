using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using to_do_backend.Dtos;
using System.Data.SQLite;
using System;
using to_do_backend.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace to_do_backend.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly BackendContext db;
        public UsersController(BackendContext backendContext)
        {
            db = backendContext;
        }

        [HttpGet, Route("admin/{id}")]
        public ActionResult<List<User>> getAdminUsers([FromRoute] int id)
        {
            var user = db.Users.Find(id);
            if (user == null) return NotFound();
            if (user.Role != "admin") return Unauthorized();

            var users = db.Users.Select(u => new UserDto()
            {
                id = u.Id,
                username = u.Username,
                password = u.Password,
                role = u.Role
            }).ToList();

            return Ok(users);
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            var users = db.Users.ToList();
            return Ok(users);
        }

        [HttpGet, Route("personal")]
        [Authorize(Roles = "admin,user")]
        public ActionResult<UserDto> getUserInfo()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return NotFound();
            if(!int.TryParse(claim.Value, out int userId)) return NotFound();
            var user = db.Users.Find(userId);
            if (user == null) return NotFound();
            var userDto = new UserDto()
            {
                id = user.Id,
                username = user.Username,
                password = user.Password,
                role = user.Role
            };

            return Ok(userDto);
        }

        [HttpPost, Route("login")]
        public ActionResult Login([FromBody] LoginRequestDto userDto)
        {
            var result = new LoginResponseDto();
            try
            {
                SQLiteConnection connection = new SQLiteConnection("Data Source=ToDoDatabase.db");
                connection.Open();
                string query = "SELECT * FROM Users WHERE Username='" +
                userDto.username + "' AND Password='" + userDto.password + "';";
                var command = connection.CreateCommand();
                command.CommandText = query;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.id = reader.GetInt32(0);
                        result.username = reader.GetString(1);
                        result.role = reader.GetString(3);
                        break;
                    }
                }
                connection.Close();
                if (result.username == null)
                {
                    throw new Exception("User not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);
        }

        [HttpPost, Route("{adminId}")]
        public ActionResult<UserDto> addUser([FromRoute] int adminId, [FromBody] UserDto userDto)
        {
            User admin = db.Users.Find(adminId);
            if (admin == null || admin.Role != "admin") return Unauthorized();
            if (String.IsNullOrWhiteSpace(userDto.username) || String.IsNullOrWhiteSpace(userDto.password)) return BadRequest("Username and password must be provided");
            if (userDto.role != "user" && userDto.role != "admin") return BadRequest("Bad role provided");
            var user = new User()
            {
                Username = userDto.username,
                Password = userDto.password,
                Role = userDto.role,
                Items = new List<ToDoItem>()
            };
            db.Users.Add(user);
            db.SaveChanges();

            return Ok(user);
        }

        [HttpPut, Route("{adminId}")]
        public ActionResult<UserDto> updateUser([FromRoute] int adminId, [FromBody] UserDto userDto)
        {
            User admin = db.Users.Find(adminId);
            if (admin == null || admin.Role != "admin") return Unauthorized();
            if (String.IsNullOrWhiteSpace(userDto.username) || String.IsNullOrWhiteSpace(userDto.password)) return BadRequest("Username and password must be provided");
            if (userDto.role != "user" && userDto.role != "admin") return BadRequest("Bad role provided");
            var user = db.Users.Find(userDto.id);
            if (user == null) return NotFound();
            user.Username = userDto.username;
            user.Password = userDto.password;
            user.Role = userDto.role;

            db.SaveChanges();
            return Ok();
        }

        [HttpDelete, Route("{adminId}/{userId}")]
        public ActionResult<UserDto> deleteUser([FromRoute] int adminId, [FromRoute] int userId)
        {
            User admin = db.Users.Find(adminId);
            if (admin == null || admin.Role != "admin") return Unauthorized();

            User user = db.Users.Find(userId);
            if (user == null) return NotFound();
            db.Users.Remove(user);
            db.SaveChanges();
            return Ok();
        }

    }
}
