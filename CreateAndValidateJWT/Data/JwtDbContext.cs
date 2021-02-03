using CreateAndValidateJWT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreateAndValidateJWT.Data
{
    public class JwtDbContext:DbContext
    {
        public JwtDbContext(DbContextOptions<JwtDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}
