﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProcessFlowSProj.API.Data;

namespace ProcessFlowSProj.API.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200310130209_InitialClasseswithFK")]
    partial class InitialClasseswithFK
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int>("StaffId");

                    b.HasKey("ApprovalLevelId");

                    b.HasIndex("OperationId");

                    b.HasIndex("StaffId");

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

                    b.ToTable("StaffEntity");
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

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("StaffLoginId");

                    b.HasIndex("StaffId");

                    b.ToTable("StaffLoginEntity");
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

                    b.ToTable("Entities");
                });

            modelBuilder.Entity("ProcessFlowSProj.API.Entities.WorkFlowTrailEntity", b =>
                {
                    b.Property<int>("WorkFlowTrailId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApprovalStatusId");

                    b.Property<int?>("ApprovedByStaffId");

                    b.Property<int?>("FromLevelId");

                    b.Property<int>("FromStaffId");

                    b.Property<int>("OperationId");

                    b.Property<int>("RequestStaffId");

                    b.Property<int>("StatusId");

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

                    b.HasOne("ProcessFlowSProj.API.Entities.StaffEntity", "StaffEntity")
                        .WithMany()
                        .HasForeignKey("StaffId")
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
