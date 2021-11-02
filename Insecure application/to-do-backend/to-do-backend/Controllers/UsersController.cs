using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using to_do_backend.Dtos;
using System.Data.SQLite;
using System;
using to_do_backend.Models;

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

            return Ok(db.Users.ToList());
        }

        [HttpGet]
        public ActionResult<List<User>> GetAllUsers()
        {
            var users = db.Users.ToList();
            return Ok(users);
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
    }
}
