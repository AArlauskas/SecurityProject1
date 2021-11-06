using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using to_do_backend.Models;
using to_do_backend.Utils;

namespace to_do_backend
{
    public class User
    {
        private User() { }

        public User(string password)
        {
            (Salt, Password) = Hasher.HashPassword(password);
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; private set; }
        public byte[] Salt { get; private set; }
        public string Role { get; set; } = "user";
        public List<ToDoItem> Items { get; set; }

        public void setNewPassword(string password)
        {
            if(!Hasher.CheckPlaintextAgainstHash(password, Password, Salt))
            {
                (Salt, Password) = Hasher.HashPassword(password);
            }
        }
    }
}
