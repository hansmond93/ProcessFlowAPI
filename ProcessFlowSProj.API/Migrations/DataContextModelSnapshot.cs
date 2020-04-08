﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProcessFlowSProj.API.Data;

namespace ProcessFlowSProj.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.ApprovalLevelEntity", b =>
                {
                    b.Property<int>("ApprovalLevelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active");

                    b.Property<int>("OperationId");

                    b.Property<int>("Position");

                    b.Property<int>("RoleId");

                    b.HasKey("ApprovalLevelId");

                    b.HasIndex("OperationId");

                    b.HasIndex("RoleId");

                    b.ToTable("ApprovalLevelEntities");
                });

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.ApprovalStatusEntity", b =>
                {
                    b.Property<int>("ApprovalStatusId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApprovalStatusName");

                    b.HasKey("ApprovalStatusId");

                    b.ToTable("ApprovalStatusEntities");
                });

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.ImagesEntity", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeAdded");

                    b.Property<string>("Description");

                    b.Property<int>("ProjectId");

                    b.Property<string>("PublicId");

                    b.Property<string>("Url");

                    b.HasKey("ImageId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ImagesEntities");
                });

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.OperationEntity", b =>
                {
                    b.Property<int>("OperationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OperationCode");

                    b.Property<string>("OperationName");

                    b.HasKey("OperationId");

                    b.ToTable("OperationEntities");
                });

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.ProjectEntity", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal?>("ApprovedAmount");

                    b.Property<string>("CompanyEmail")
                        .IsRequired();

                    b.Property<string>("CompanyName")
                        .IsRequired();

                    b.Property<string>("ContactAddress")
                        .IsRequired();

                    b.Property<string>("ContactNumber")
                        .IsRequired();

                    b.Property<string>("ContactPerson")
                        .IsRequired();

                    b.Property<int>("CreatedBy");

                    b.Property<DateTime>("DateTimeCreated");

                    b.Property<DateTime?>("DateTimeDeleted");

                    b.Property<DateTime?>("DateTimeModified");

                    b.Property<int?>("DeletedBy");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<int>("DurationInMonths");

                    b.Property<bool>("IsDeleted");

                    b.Property<int?>("LastModifiedBy");

                    b.Property<string>("Location")
                        .IsRequired();

                    b.Property<string>("ProjectTitle")
                        .IsRequired();

                    b.Property<decimal>("ProposedAmount");

                    b.HasKey("ProjectId");

                    b.ToTable("ProjectEntities");
                });

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.StaffEntity", b =>
                {
                    b.Property<int>("StaffId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("Gender")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("MiddleName");

                    b.Property<int>("RoleId");

                    b.Property<string>("StaffCode");

                    b.Property<int?>("StaffLoginEntityId");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("StaffId");

                    b.HasIndex("RoleId");

                    b.HasIndex("StaffLoginEntityId");

                    b.ToTable("StaffEntities");
                });

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.StaffLoginEntity", b =>
                {
                    b.Property<int>("StaffLoginId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired();

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired();

                    b.Property<int>("StaffId");

                    b.HasKey("StaffLoginId");

                    b.HasIndex("StaffId");

                    b.ToTable("StaffLoginEntities");
                });

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.StaffRoleEntity", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleCode");

                    b.Property<string>("RoleName");

                    b.HasKey("RoleId");

                    b.ToTable("StaffRoleEntities");
                });

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.WorkFLowStatusEntity", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StatusName")
                        .IsRequired();

                    b.HasKey("StatusId");

                    b.ToTable("WorkFLowStatusEntity");
                });

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.WorkFlowTrailEntity", b =>
                {
                    b.Property<int>("WorkFlowTrailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("ApprovalStatusId");

                    b.Property<int?>("ApprovedByStaffId");

                    b.Property<string>("Comment");

                    b.Property<DateTime>("DateTimeApproved");

                    b.Property<int?>("FromLevelId");

                    b.Property<int?>("FromStaffId");

                    b.Property<int>("OperationId");

                    b.Property<int>("RequestStaffId");

                    b.Property<byte>("StatusId");

                    b.Property<int>("TargetId");

                    b.Property<int?>("ToLevelId");

                    b.Property<int?>("ToStaffId");

                    b.HasKey("WorkFlowTrailId");

                    b.HasIndex("OperationId");

                    b.ToTable("WorkFlowTrailEntities");
                });

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.ApprovalLevelEntity", b =>
                {
                    b.HasOne("ProcessFlowSProj.API.Entities.OperationEntity", "OperationEntity")
                        .WithMany()
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProcessFlowSProj.API.Entities.StaffRoleEntity", "StaffRoleEntity")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.ImagesEntity", b =>
                {
                    b.HasOne("ProcessFlowSProj.API.Entities.ProjectEntity", "ProjectEntity")
                        .WithMany("ImagesEntities")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.StaffEntity", b =>
                {
                    b.HasOne("ProcessFlowSProj.API.Entities.StaffRoleEntity", "StaffRoleEntity")
                        .WithMany("StaffEntities")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ProcessFlowSProj.API.Entities.StaffLoginEntity", "StaffLoginEntity")
                        .WithMany()
                        .HasForeignKey("StaffLoginEntityId");
                });

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.StaffLoginEntity", b =>
                {
                    b.HasOne("ProcessFlowSProj.API.Entities.StaffEntity", "StaffEntity")
                        .WithMany()
                        .HasForeignKey("StaffId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.WorkFlowTrailEntity", b =>
                {
                    b.HasOne("ProcessFlowSProj.API.Entities.OperationEntity", "OperationEntity")
                        .WithMany()
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
