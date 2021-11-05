using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using to_do_backend.Models;

namespace to_do_backend
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "user";
        public List<ToDoItem> Items { get; set; }
    }
}
