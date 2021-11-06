using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using to_do_backend.EntityConfigurations;
using to_do_backend.Models;

namespace to_do_backend
{
    public class BackendContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ToDoItem> Items { get; set; }

        public BackendContext(DbContextOptions<BackendContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            List<User> users = new List<User>();
            users.Add(new User("password")
            {
                Id = 1,
                Username = "Aurimas",
                Role = "admin",
                Items = new List<ToDoItem>()
            });
            users.Add(new User("password")
            {
                Id = 2,
                Username = "Ana",
                Role = "user",
                Items = new List<ToDoItem>()
            });
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration(users));
            modelBuilder.ApplyConfiguration(new ToDoItemEntityConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=ToDoDatabase.db");
        } 
    }
}
