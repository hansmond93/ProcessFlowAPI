using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProcessFlowSProj.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessFlowSProj.API.Data
{
    public class DataContext : IdentityDbContext<StaffEntity, Role, int,
                                IdentityUserClaim<int>, StaffUserRole, IdentityUserLogin<int>,
                                IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<ApprovalLevelEntity> ApprovalLevelEntities { get; set; }
        public DbSet<ApprovalStatusEntity> ApprovalStatusEntities { get; set; }
        public DbSet<OperationEntity> OperationEntities { get; set; }
        public DbSet<StaffEntity> StaffEntities { get; set; }
        public DbSet<RoleEntity> RoleEntities { get; set; }
        public DbSet<WorkFLowStatusEntity> WorkFLowStatusEntity { get; set; }
        public DbSet<WorkFlowTrailEntity> WorkFlowTrailEntities { get; set; }
        public DbSet<ImagesEntity> ImagesEntities { get; set; }
        public DbSet<ProjectEntity> ProjectEntities { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StaffUserRole>(sue =>
            {
                sue.HasKey(ur => new { ur.UserId, ur.RoleId });

                sue.HasOne(ur => ur.StaffEntity)
                    .WithMany(r => r.StaffUserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

                sue.HasOne(ur => ur.Role)
                    .WithMany(r => r.StaffUserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            builder.Entity<StaffEntity>()
                    .ToTable(nameof(StaffEntity))
                    .HasKey(u => u.Id);

            builder.Entity<RoleEntity>()
                    .ToTable(nameof(RoleEntity))
                    .HasKey(u => u.Id);

            builder.Entity<StaffEntity>()
                    .HasOne(r => r.RoleEntity)
                    .WithMany(s => s.StaffEntities)
                    .HasForeignKey(x => x.RoleEntityId)
                    .OnDelete(DeleteBehavior.Restrict);


            builder.Entity<WorkFlowTrailEntity>()
                .ToTable(nameof(WorkFlowTrailEntity))
                .HasKey(w => w.WorkFlowTrailId);

            builder.Entity<OperationEntity>()
                .HasOne(x => x.WorkFlowTrailEntity)
                .WithOne(x => x.OperationEntity)
                .HasForeignKey<WorkFlowTrailEntity>( w => w.OperationId);

            builder.Entity<ApprovalLevelEntity>()
                .HasOne(x => x.WorkFlowTrailEntityFromLevel)
                .WithOne(x => x.FromLevel)
                .HasForeignKey<WorkFlowTrailEntity>(w => w.FromLevelId);

            builder.Entity<ApprovalLevelEntity>()
                .HasOne(x => x.WorkFlowTrailEntityToLevel)
                .WithOne(x => x.ToLevel)
                .HasForeignKey<WorkFlowTrailEntity>( w => w.ToLevelId);

            builder.Entity<StaffEntity>()
                .HasOne(x => x.WorkFlowTrailEntityApprovedBy)
                .WithOne(x => x.ApprovedByStaff)
                .HasForeignKey<WorkFlowTrailEntity>(w => w.ApprovedByStaffId);

            builder.Entity<StaffEntity>()
                .HasOne(x => x.WorkFlowTrailEntityFromStaff)
                .WithOne(x => x.FromStaff)
                .HasForeignKey< WorkFlowTrailEntity>(w => w.FromStaffId);

            builder.Entity<StaffEntity>()
                .HasOne(x => x.WorkFlowTrailEntityToStaff)
                .WithOne(x => x.ToStaff)
                .HasForeignKey< WorkFlowTrailEntity>(w => w.ToStaffId);

            builder.Entity<StaffEntity>()
                .HasOne(x => x.WorkFlowTrailEntityRequestStaff)
                .WithOne(x => x.RequestStaff)
                .HasForeignKey<WorkFlowTrailEntity>(w => w.RequestStaffId);

            builder.Entity<ApprovalStatusEntity>()
                .HasOne(x => x.WorkFlowTrailEntity)
                .WithOne(x => x.ApprovalStatus)
                .HasForeignKey<WorkFlowTrailEntity>(w => w.ApprovalStatusId);

            builder.Entity<WorkFLowStatusEntity>()
                .HasOne(x => x.WorkFlowTrailEntity)
                .WithOne(x => x.Status)
                .HasForeignKey<WorkFlowTrailEntity>(w => w.StatusId);

            builder.Entity<StaffEntity>()
                .Property(p => p.UserName)
                .IsRequired();


        }
    }
}
