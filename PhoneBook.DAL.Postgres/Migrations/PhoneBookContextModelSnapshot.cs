﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PhoneBook.DAL;
using PhoneBook.Model;

#nullable disable

namespace PhoneBook.DAL.Migrations
{
    [DbContext(typeof(PhoneBookContext))]
    partial class PhoneBookContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "PhoneType", new[] { "home", "no_type", "work" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PhoneBook.Model.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Patronymic")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("PhoneBook.Model.Phone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PersonId")
                        .HasColumnType("integer");

                    b.Property<PhoneType>("Type")
                        .HasColumnType("\"PhoneType\"");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Phones");
                });

            modelBuilder.Entity("PhoneBook.Model.Phone", b =>
                {
                    b.HasOne("PhoneBook.Model.Person", "Person")
                        .WithMany("Phones")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Person");
                });

            modelBuilder.Entity("PhoneBook.Model.Person", b =>
                {
                    b.Navigation("Phones");
                });
#pragma warning restore 612, 618
        }
    }
}
