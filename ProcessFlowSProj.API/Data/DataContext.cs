using Microsoft.EntityFrameworkCore;
using ProcessFlowSProj.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Value> Values { get; set; }
        public DbSet<UserLoginEntity> UserLoginEntities { get; set; }
        public DbSet<UserEntity> UserEntities { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}
    }
}
