﻿// <auto-generated />
using System;
using JodyCore2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace JodyCore2.Data.Migrations
{
    [DbContext(typeof(JodyContext))]
    partial class JodyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("JodyCore2.Data.Dto.GameDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int?>("AwayDtoId")
                        .HasColumnType("integer");

                    b.Property<int>("AwayScore")
                        .HasColumnType("integer");

                    b.Property<bool>("CanTie")
                        .HasColumnType("boolean");

                    b.Property<bool>("Complete")
                        .HasColumnType("boolean");

                    b.Property<int>("Day")
                        .HasColumnType("integer");

                    b.Property<int?>("HomeDtoId")
                        .HasColumnType("integer");

                    b.Property<int>("HomeScore")
                        .HasColumnType("integer");

                    b.Property<Guid>("Identifier")
                        .HasColumnType("uuid");

                    b.Property<bool>("Processed")
                        .HasColumnType("boolean");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AwayDtoId");

                    b.HasIndex("HomeDtoId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("JodyCore2.Data.Dto.TeamDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<Guid>("Identifier")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Skill")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("JodyCore2.Data.Dto.GameDto", b =>
                {
                    b.HasOne("JodyCore2.Data.Dto.TeamDto", "AwayDto")
                        .WithMany()
                        .HasForeignKey("AwayDtoId");

                    b.HasOne("JodyCore2.Data.Dto.TeamDto", "HomeDto")
                        .WithMany()
                        .HasForeignKey("HomeDtoId");

                    b.Navigation("AwayDto");

                    b.Navigation("HomeDto");
                });
#pragma warning restore 612, 618
        }
    }
}
