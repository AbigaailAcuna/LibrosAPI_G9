﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibrosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace LibrosAPI.Tests
{
    public static class Setup
    {
        public static LibrosDBContext GetInMemoryDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<LibrosDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new LibrosDBContext(options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
