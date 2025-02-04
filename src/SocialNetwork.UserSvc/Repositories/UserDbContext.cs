﻿using Microsoft.EntityFrameworkCore;
using SocialNetwork.UserSvc.Entities;

namespace SocialNetwork.UserSvc.Repositories
{
    public class UserDbContext : DbContext
    {
        DbSet<User> Users => Set<User>();
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .OwnsOne(a => a.Email);
        }
    }
}
