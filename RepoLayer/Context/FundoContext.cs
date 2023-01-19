using Microsoft.EntityFrameworkCore;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Context
{
    public  class FundoContext : DbContext
    {
        
            public FundoContext(DbContextOptions options) : base(options)
            {
            }
            public DbSet<UserEntity> UserTable { get; set; }
            public DbSet<NoteEntity> NoteTable { get; set; }
            public DbSet<CollabEntity> CollabTable { get; set; }
             public DbSet<LableEntity> LableTable { get; set; }
    }
}

