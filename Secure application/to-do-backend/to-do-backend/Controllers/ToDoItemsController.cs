using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using to_do_backend.Dtos;
using to_do_backend.Models;
using System.Data.Entity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace to_do_backend.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ToDoItemsController : ControllerBase
    {
        private readonly BackendContext db;
        public ToDoItemsController(BackendContext backendContext)
        {
            db = backendContext;
        }

        [HttpGet, Route("admin")]
        [Authorize(Roles = "admin,user")]
        public ActionResult<List<ToDoItemDto>> getToDoItems()
        {
            var user = getUserFromClaim();
            var items = db.Items.Select(item => new ToDoItemDto()
            {
                id = item.Id,
                title = item.Title,
                description = item.Description,
                isDone = item.IsDone,
            }).ToList();

            return Ok(items);
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public ActionResult<List<ToDoItemDto>> getUsersToDoItems()
        {
            var user = getUserFromClaim();
            if (user == null) return NotFound();
            var items = db.Items.Where(i => i.UserId == user.Id)
                .Select(i => new ToDoItemDto()
                {
                    id = i.Id,
                    title = i.Title,
                    description = i.Description,
                    isDone = i.IsDone,
                    username = user.Username
                }).ToList();

            return Ok(items);
        }

        [HttpGet, Route("{id}")]
        [Authorize(Roles = "admin,user")]
        public ActionResult<ToDoItemDto> getToDoItem([FromRoute] int id)
        {
            var user = getUserFromClaim();
            if (user == null) return BadRequest("User not found");

            var item = db.Items.Find(id);
            if (item == null) return NotFound();

            if (user.Id != item.UserId && user.Role != "admin") return Unauthorized();

            var result = new ToDoItemDto()
            {
                id = item.Id,
                title = item.Title,
                description = item.Description,
                isDone = item.IsDone,
                username = user.Username
            };
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin,user")]
        public ActionResult addToDoItem([FromBody] ToDoItemDto item)
        {
            var user = db.Users.Find(item.userId);
            if (user == null) return BadRequest("user id must be provided");
            if (item.title.Length > 50 || item.description.Length > 150) return BadRequest();
            var newItem = new ToDoItem()
            {
                Title = item.title,
                Description = item.description,
                UserId = item.userId
            };
            db.Items.Add(newItem);
            db.SaveChanges();
            return Ok();
        }

        [HttpPut, Route("{id}")]
        [Authorize(Roles = "user,admin")]
        public ActionResult<ToDoItem> updateToDoItem([FromRoute] int id, [FromBody] ToDoItemDto item)
        {
            var existingItem = db.Items.Find(id);
            if (existingItem == null) return NotFound();

            var user = getUserFromClaim();
            if (user == null) return Unauthorized();
            if (user.Role == "user" && user.Id != existingItem.UserId) return Unauthorized();

            if (item.title.Length > 50 || item.description.Length > 150) return BadRequest();

            existingItem.Title = item.title;
            existingItem.Description = item.description;

            db.SaveChanges();
            return Ok();
        }

        [HttpPut, Route("toggle/{id}")]
        [Authorize(Roles = "admin,user")]
        public ActionResult<ToDoItem> toggleToDoItem([FromRoute] int id)
        {
            var existingItem = db.Items.Find(id);
            if (existingItem == null) return NotFound();

            var user = getUserFromClaim();
            if (user == null) return Unauthorized();
            if (user.Role == "user" && user.Id != existingItem.UserId) return Unauthorized();

            existingItem.IsDone = !existingItem.IsDone;
            db.SaveChanges();
            return Ok();
        }

        [HttpDelete, Route("{id}")]
        [Authorize(Roles = "admin,user")]
        public ActionResult deleteToDoItem([FromRoute] int id)
        {
            var item = db.Items.Find(id);
            if (item == null) return NotFound();

            var user = getUserFromClaim();
            if (user == null) return Unauthorized();
            if (user.Role == "user" && user.Id != item.UserId) return Unauthorized();

            db.Items.Remove(item);
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
