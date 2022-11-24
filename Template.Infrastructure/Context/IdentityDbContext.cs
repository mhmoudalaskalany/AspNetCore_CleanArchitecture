﻿using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public partial class BackendCoreDbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }

    }
}