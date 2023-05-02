﻿// <auto-generated />
using System;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LlamaJournal.Migrations
{
    [DbContext(typeof(ModelsContext))]
    [Migration("20230502141805_update_user_table")]
    partial class update_user_table
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DataLayer.Models.Attendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<long>("DisciplineId")
                        .HasColumnType("bigint");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("UserId");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("DataLayer.Models.Discipline", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(12)
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Disciplines");
                });

            modelBuilder.Entity("DataLayer.Models.Grade", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(12)
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Comment")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("Date");

                    b.Property<long>("DisciplineId")
                        .HasColumnType("bigint");

                    b.Property<int>("Score")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("DisciplineId");

                    b.HasIndex("UserId");

                    b.ToTable("Grades");
                });

            modelBuilder.Entity("DataLayer.Models.Group", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(12)
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("OrganizationId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("DataLayer.Models.Organization", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(12)
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("DataLayer.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("CompleteRegistration")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("character varying(250)");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("GroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DisciplineGroup", b =>
                {
                    b.Property<long>("DisciplinesId")
                        .HasColumnType("bigint");

                    b.Property<long>("GroupsId")
                        .HasColumnType("bigint");

                    b.HasKey("DisciplinesId", "GroupsId");

                    b.HasIndex("GroupsId");

                    b.ToTable("DisciplineGroup");
                });

            modelBuilder.Entity("DisciplineUser", b =>
                {
                    b.Property<long>("TeacherDisciplinesId")
                        .HasColumnType("bigint");

                    b.Property<string>("TeachersId")
                        .HasColumnType("character varying(50)");

                    b.HasKey("TeacherDisciplinesId", "TeachersId");

                    b.HasIndex("TeachersId");

                    b.ToTable("DisciplineUser");
                });

            modelBuilder.Entity("DataLayer.Models.Attendance", b =>
                {
                    b.HasOne("DataLayer.Models.Discipline", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Models.Grade", b =>
                {
                    b.HasOne("DataLayer.Models.Discipline", "Discipline")
                        .WithMany()
                        .HasForeignKey("DisciplineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.User", "User")
                        .WithMany("Grades")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Discipline");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DataLayer.Models.Group", b =>
                {
                    b.HasOne("DataLayer.Models.Organization", "Organization")
                        .WithMany("Groups")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("DataLayer.Models.User", b =>
                {
                    b.HasOne("DataLayer.Models.Group", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("DisciplineGroup", b =>
                {
                    b.HasOne("DataLayer.Models.Discipline", null)
                        .WithMany()
                        .HasForeignKey("DisciplinesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.Group", null)
                        .WithMany()
                        .HasForeignKey("GroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DisciplineUser", b =>
                {
                    b.HasOne("DataLayer.Models.Discipline", null)
                        .WithMany()
                        .HasForeignKey("TeacherDisciplinesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataLayer.Models.User", null)
                        .WithMany()
                        .HasForeignKey("TeachersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataLayer.Models.Group", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("DataLayer.Models.Organization", b =>
                {
                    b.Navigation("Groups");
                });

            modelBuilder.Entity("DataLayer.Models.User", b =>
                {
                    b.Navigation("Grades");
                });
#pragma warning restore 612, 618
        }
    }
}
