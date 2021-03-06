﻿// <auto-generated />
using System;
using Chatter.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Chatter.DAL.Migrations
{
    [DbContext(typeof(ChatterDBContext))]
    [Migration("20190425162157_byte_pass")]
    partial class byte_pass
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Chatter.DAL.Model.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateTime");

                    b.Property<Guid?>("PostId");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Chatter.DAL.Model.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CommentId");

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<Guid?>("PostId");

                    b.Property<int>("Size");

                    b.Property<int>("Type");

                    b.Property<Guid?>("UploadedById");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UploadedById");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Chatter.DAL.Model.Group", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<Guid?>("OrganizationId");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("Chatter.DAL.Model.ManyToManyTables.Admin_Group", b =>
                {
                    b.Property<Guid>("AdminRefId");

                    b.Property<Guid>("GroupRefId");

                    b.Property<Guid>("Id");

                    b.HasKey("AdminRefId", "GroupRefId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("GroupRefId");

                    b.ToTable("Admin_Group");
                });

            modelBuilder.Entity("Chatter.DAL.Model.ManyToManyTables.Admin_Organization", b =>
                {
                    b.Property<Guid>("AdminRefId");

                    b.Property<Guid>("OrganizationRefId");

                    b.Property<Guid>("Id");

                    b.HasKey("AdminRefId", "OrganizationRefId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("OrganizationRefId");

                    b.ToTable("Admin_Organization");
                });

            modelBuilder.Entity("Chatter.DAL.Model.ManyToManyTables.User_Group", b =>
                {
                    b.Property<Guid>("UserRefId");

                    b.Property<Guid>("GroupRefId");

                    b.Property<Guid>("Id");

                    b.HasKey("UserRefId", "GroupRefId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("GroupRefId");

                    b.ToTable("User_Group");
                });

            modelBuilder.Entity("Chatter.DAL.Model.ManyToManyTables.User_Organization", b =>
                {
                    b.Property<Guid>("UserRefId");

                    b.Property<Guid>("OrganizationRefId");

                    b.Property<Guid>("Id");

                    b.HasKey("UserRefId", "OrganizationRefId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("OrganizationRefId");

                    b.ToTable("User_Organization");
                });

            modelBuilder.Entity("Chatter.DAL.Model.Organization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("Chatter.DAL.Model.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("DateTime");

                    b.Property<Guid?>("GroupId");

                    b.Property<string>("PostHeader");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Chatter.DAL.Model.SignInLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("SignInLogTime");

                    b.Property<int>("SignLogCode");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("SingInLogs");
                });

            modelBuilder.Entity("Chatter.DAL.Model.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("CommentId");

                    b.Property<Guid?>("PostId");

                    b.Property<Guid?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("Chatter.DAL.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("FirstName");

                    b.Property<int>("Gender");

                    b.Property<string>("LastName");

                    b.Property<byte[]>("Password");

                    b.Property<DateTime>("RegistrationDate");

                    b.Property<byte[]>("Salt");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.HasAlternateKey("Email")
                        .HasName("AlternateKey_Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Chatter.DAL.Model.Comment", b =>
                {
                    b.HasOne("Chatter.DAL.Model.Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId");

                    b.HasOne("Chatter.DAL.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Chatter.DAL.Model.File", b =>
                {
                    b.HasOne("Chatter.DAL.Model.Comment", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentId");

                    b.HasOne("Chatter.DAL.Model.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId");

                    b.HasOne("Chatter.DAL.Model.User", "UploadedBy")
                        .WithMany()
                        .HasForeignKey("UploadedById");
                });

            modelBuilder.Entity("Chatter.DAL.Model.Group", b =>
                {
                    b.HasOne("Chatter.DAL.Model.Organization")
                        .WithMany("Groups")
                        .HasForeignKey("OrganizationId");
                });

            modelBuilder.Entity("Chatter.DAL.Model.ManyToManyTables.Admin_Group", b =>
                {
                    b.HasOne("Chatter.DAL.Model.User", "Admin")
                        .WithMany("AdminGroups")
                        .HasForeignKey("AdminRefId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Chatter.DAL.Model.Group", "Group")
                        .WithMany("Admins")
                        .HasForeignKey("GroupRefId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Chatter.DAL.Model.ManyToManyTables.Admin_Organization", b =>
                {
                    b.HasOne("Chatter.DAL.Model.User", "Admin")
                        .WithMany("AdminOrganizations")
                        .HasForeignKey("AdminRefId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Chatter.DAL.Model.Organization", "Organization")
                        .WithMany("Admins")
                        .HasForeignKey("OrganizationRefId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Chatter.DAL.Model.ManyToManyTables.User_Group", b =>
                {
                    b.HasOne("Chatter.DAL.Model.Group", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupRefId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Chatter.DAL.Model.User", "User")
                        .WithMany("Groups")
                        .HasForeignKey("UserRefId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Chatter.DAL.Model.ManyToManyTables.User_Organization", b =>
                {
                    b.HasOne("Chatter.DAL.Model.Organization", "Organization")
                        .WithMany("Users")
                        .HasForeignKey("OrganizationRefId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Chatter.DAL.Model.User", "User")
                        .WithMany("Organizations")
                        .HasForeignKey("UserRefId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Chatter.DAL.Model.Post", b =>
                {
                    b.HasOne("Chatter.DAL.Model.Group")
                        .WithMany("Posts")
                        .HasForeignKey("GroupId");

                    b.HasOne("Chatter.DAL.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Chatter.DAL.Model.SignInLog", b =>
                {
                    b.HasOne("Chatter.DAL.Model.User")
                        .WithMany("SignInLogs")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Chatter.DAL.Model.Tag", b =>
                {
                    b.HasOne("Chatter.DAL.Model.Comment", "Comment")
                        .WithMany()
                        .HasForeignKey("CommentId");

                    b.HasOne("Chatter.DAL.Model.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId");

                    b.HasOne("Chatter.DAL.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
