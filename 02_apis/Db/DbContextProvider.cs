using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02_apis.Model;
using Microsoft.EntityFrameworkCore;

namespace _02_apis.Db
{
    public class DbContextProvider : DbContext
    {
        public DbContextProvider(DbContextOptions<DbContextProvider> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}