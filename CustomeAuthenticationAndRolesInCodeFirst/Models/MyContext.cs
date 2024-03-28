using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CustomeAuthenticationAndRolesInCodeFirst.Models
{
    public class MyContext : DbContext
    {
        public MyContext() : base("DbCon")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}