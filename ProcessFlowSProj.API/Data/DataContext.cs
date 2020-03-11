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

        public DbSet<StaffLoginEntity> UserLoginEntities { get; set; }
        public DbSet<StaffEntity> UserEntities { get; set; }
        public DbSet<ApprovalLevelEntity> ApprovalLevelEntities { get; set; }
        public DbSet<ApprovalStatusEntity> ApprovalStatusEntities { get; set; }
        public DbSet<OperationEntity> OperationEntities { get; set; }
        public DbSet<StaffEntity> StaffEntities { get; set; }
        public DbSet<StaffLoginEntity> StaffLoginEntities { get; set; }
        public DbSet<StaffRoleEntity> StaffRoleEntities { get; set; }
        public DbSet<WorkFLowStatusEntity> Entities { get; set; }
        public DbSet<WorkFlowTrailEntity> WorkFlowTrailEntities { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{

        //}
    }
}
