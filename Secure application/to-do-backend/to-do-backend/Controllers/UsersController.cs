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

        [HttpGet, Route("personal")]
        [Authorize(Roles = "admin,user")]
        public ActionResult<UserDto> getUserInfo()
        {
            var user = getUserFromClaim();
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

        [HttpGet, Route("admin")]
        [Authorize(Roles = "admin")]
        public ActionResult<List<User>> getAdminUsers()
        {
            var users = db.Users.Select(u => new UserDto()
            {
                id = u.Id,
                username = u.Username,
                password = u.Password,
                role = u.Role
            }).ToList();

            return Ok(users);
        }

        [HttpPost, Route("admin")]
        [Authorize(Roles = "admin")]
        public ActionResult<UserDto> addUser([FromBody] UserDto userDto)
        {
            if (String.IsNullOrWhiteSpace(userDto.username) || String.IsNullOrWhiteSpace(userDto.password)) return BadRequest("Username and password must be provided");
            if (userDto.role != "user" && userDto.role != "admin") return BadRequest("Bad role provided");
            var user = new User(userDto.password)
            {
                Username = userDto.username,
                Role = userDto.role,
                Items = new List<ToDoItem>()
            };
            db.Users.Add(user);
            db.SaveChanges();

            return Ok(user);
        }

        [HttpPut, Route("admin")]
        [Authorize(Roles = "admin")]
        public ActionResult<UserDto> updateUser([FromBody] UserDto userDto)
        {
            if (String.IsNullOrWhiteSpace(userDto.username) || String.IsNullOrWhiteSpace(userDto.password)) return BadRequest("Username and password must be provided");
            if (userDto.role != "user" && userDto.role != "admin") return BadRequest("Bad role provided");
            var user = db.Users.Find(userDto.id);
            if (user == null) return NotFound();
            user.Username = userDto.username;
            user.setNewPassword(userDto.password);
            user.Role = userDto.role;

            db.SaveChanges();
            return Ok();
        }

        [HttpDelete, Route("admin/{userId}")]
        [Authorize(Roles = "admin")]
        public ActionResult<UserDto> deleteUser([FromRoute] int id)
        {
            User user = db.Users.Find(id);
            if (user == null) return NotFound();
            db.Users.Remove(user);
            db.SaveChanges();
            return Ok();
        }

        private User getUserFromClaim()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim == null) return null;
            if (!int.TryParse(claim.Value, out int userId)) return null;
            var user = db.Users.Find(userId);
            return user;
        }
    }
}
