using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace to_do_backend.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        private List<User> users;

        public UserEntityConfiguration() 
        {
            users = new List<User>();
        }
        public UserEntityConfiguration(List<User> users)
        {
            this.users = users;
        }
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();
            builder.Property(u => u.Username);
            builder.Property(u => u.Password);
            builder.Property(u => u.Role);

            builder.HasMany(u => u.Items)
                .WithOne(i => i.User)
                .HasForeignKey(u => u.Id);

            builder.HasData(users);
        }
    }
}
