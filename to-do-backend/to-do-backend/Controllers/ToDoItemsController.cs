using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using to_do_backend.Dtos;
using to_do_backend.Models;
using System.Data.Entity;

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

        [HttpGet, Route("admin/{id}")]
        public ActionResult<List<ToDoItemDto>> getToDoItems([FromRoute] int id)
        {
            var user = db.Users.Find(id);
            if (user == null) return NotFound();
            if (user.Role != "admin") return Unauthorized();

            var items = db.Items.Select(item => new ToDoItemDto()
            {
                id = item.Id,
                title = item.Title,
                description = item.Description,
                isDone = item.IsDone,
                username = user.Username
            }).ToList();

            return Ok(items);
        }

        [HttpGet]
        public ActionResult<List<ToDoItemDto>> getUsersToDoItems(int userId)
        {
            var user = db.Users.Find(userId);
            if (user == null) return NotFound();
            var items = user.Items.Select(i => new ToDoItemDto()
            {
                id = i.Id,
                title = i.Title,
                description = i.Description,
                isDone = i.IsDone,
                username = i.User.Username
            }).ToList();

            return Ok(items);
        }

        [HttpGet, Route("{id}")]
        public ActionResult<ToDoItemDto> getToDoItem([FromRoute] int id)
        {
            var item = db.Items.Find(id);
            if (item == null) return NotFound();
            var user = db.Users.Find(item.UserId);
            if (user == null) return BadRequest("User not found");
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
        public ActionResult addToDoItem([FromBody] ToDoItemDto item)
        {
            var user = db.Users.Find(item.userId);
            if (user == null) return BadRequest("user id must be provided");
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

        [HttpPatch, Route("{id}")]
        public ActionResult<ToDoItem> updateToDoItem([FromRoute] int id, [FromBody] ToDoItemDto item)
        {
            var existingItem = db.Items.Find(id);
            if (existingItem == null) return NotFound();

            existingItem.Title = item.title;
            existingItem.Description = item.description;
            existingItem.IsDone = item.isDone;

            db.SaveChanges();
            return Ok(existingItem);
        }

        [HttpPatch, Route("toggle/{id}")]
        public ActionResult<ToDoItem> toggleToDoItem([FromRoute] int id)
        {
            var existingItem = db.Items.Find(id);
            if (existingItem == null) return NotFound();

            existingItem.IsDone = !existingItem.IsDone;
            db.SaveChanges();
            return Ok(existingItem);
        }

        [HttpDelete, Route("{id}")]
        public ActionResult deleteToDoItem([FromRoute] int id)
        {
            var item = db.Items.Find(id);
            if (item == null) return NotFound();
            db.Items.Remove(item);
            db.SaveChanges();
            return Ok();
        }
    }
}
