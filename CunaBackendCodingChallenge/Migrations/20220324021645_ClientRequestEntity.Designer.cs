﻿// <auto-generated />
using System;
using CunaBackendCodingChallenge.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CunaBackendCodingChallenge.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220324021645_ClientRequestEntity")]
    partial class ClientRequestEntity
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CunaBackendCodingChallenge.ClientRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedDateTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ClientRequests");
                });

            modelBuilder.Entity("CunaBackendCodingChallenge.ServiceReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ClientRequestId")
                        .HasColumnType("int");

                    b.Property<string>("Detail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClientRequestId")
                        .IsUnique();

                    b.ToTable("ServiceReports");
                });

            modelBuilder.Entity("CunaBackendCodingChallenge.ServiceReport", b =>
                {
                    b.HasOne("CunaBackendCodingChallenge.ClientRequest", "ClientRequest")
                        .WithOne("ServiceReport")
                        .HasForeignKey("CunaBackendCodingChallenge.ServiceReport", "ClientRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClientRequest");
                });

            modelBuilder.Entity("CunaBackendCodingChallenge.ClientRequest", b =>
                {
                    b.Navigation("ServiceReport");
                });
#pragma warning restore 612, 618
        }
    }
}
