using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManager.Entities.Concrete;

namespace UserManager.DataAccess.Conrete
{
    public class UserDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; DataBase=UserDb;");
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }
        
    }
}
