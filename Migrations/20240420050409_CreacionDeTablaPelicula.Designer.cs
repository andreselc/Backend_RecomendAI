﻿// <auto-generated />
using System;
using IARecommendAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IARecommendAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240420050409_CreacionDeTablaPelicula")]
    partial class CreacionDeTablaPelicula
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IARecommendAPI.Modelos.Pelicula", b =>
                {
                    b.Property<int>("Id_pelicula")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_pelicula"));

                    b.Property<string>("Cartel_path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Fecha_estreno")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titulo_original")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_pelicula");

                    b.ToTable("Pelicula");
                });
#pragma warning restore 612, 618
        }
    }
}
