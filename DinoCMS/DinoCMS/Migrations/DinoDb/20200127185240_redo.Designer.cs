﻿// <auto-generated />
using DinoCMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DinoCMS.Migrations.DinoDb
{
    [DbContext(typeof(DinoDbContext))]
    [Migration("20200127185240_redo")]
    partial class redo
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DinoCMS.Models.Dinosaur", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Additionalinfo");

                    b.Property<string>("Behavior")
                        .IsRequired();

                    b.Property<int>("Diet");

                    b.Property<string>("Image");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NeedToKnow")
                        .IsRequired();

                    b.Property<string>("PackLimits");

                    b.Property<string>("SocialInteraction")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Dinosaur");
                });
#pragma warning restore 612, 618
        }
    }
}