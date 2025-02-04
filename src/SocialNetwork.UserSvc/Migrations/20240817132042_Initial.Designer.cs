﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SocialNetwork.UserSvc.Repositories;


#nullable disable

namespace SocialNetwork.WebAPI.AccountService.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20240817132042_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("SocialNetwork.WebAPI.AccountService.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("SocialNetwork.WebAPI.AccountService.Account", b =>
                {
                    b.OwnsOne("SocialNetwork.WebAPI.AccountService.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("AccountId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("AccountId");

                            b1.ToTable("Accounts");

                            b1.WithOwner()
                                .HasForeignKey("AccountId");
                        });

                    b.Navigation("Email")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
