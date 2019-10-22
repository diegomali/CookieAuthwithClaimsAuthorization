using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IContext
    {
        public DbSet<City> City { get; set; }
        public DbSet<Classification> Classification { get; }
        public DbSet<Customer> Customer { get; }
        public DbSet<Gender> Gender { get; }
        public DbSet<Region> Region { get; }
        public DbSet<UserRole> UserRole { get; }
        public DbSet<UserSys> UserSys { get; }
    }
}
