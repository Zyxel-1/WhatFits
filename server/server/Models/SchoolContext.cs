﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace server.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext()
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Standard> Standards { get; set; }

    }
}