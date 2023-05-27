﻿using Microsoft.EntityFrameworkCore;

namespace DevagramCSharp.Models
{
    public class DevagramContext : DbContext
    {
        //construtor
        public DevagramContext(DbContextOptions<DevagramContext> option) : base(option)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }


    }
}