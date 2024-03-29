﻿// <auto-generated />
using System;
using DocumentLight.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DocumentLight.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190604125623_StaggingDB")]
    partial class StaggingDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DocumentLight.Core.Entities.File", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("FileName");

                    b.Property<string>("RelativePath");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("DocumentLight.Core.Entities.Template", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<Guid?>("FileId");

                    b.Property<string>("Thumbnail");

                    b.Property<string>("Title");

                    b.Property<DateTime>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("FileId");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("DocumentLight.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<string>("ForgotPasswordToken");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsEmailVerified");

                    b.Property<string>("Password");

                    b.Property<DateTime>("UpdatedAt");

                    b.Property<string>("VerifyEmailToken");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DocumentLight.Core.Entities.Template", b =>
                {
                    b.HasOne("DocumentLight.Core.Entities.File", "File")
                        .WithMany()
                        .HasForeignKey("FileId");
                });
#pragma warning restore 612, 618
        }
    }
}
