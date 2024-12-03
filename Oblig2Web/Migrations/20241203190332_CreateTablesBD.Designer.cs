﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oblig2Web.Datos;

#nullable disable

namespace Oblig2Web.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241203190332_CreateTablesBD")]
    partial class CreateTablesBD
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Oblig2Web.Modelos.Habitacion", b =>
                {
                    b.Property<int>("IdHabitacion")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHabitacion"), 1L, 1);

                    b.Property<int>("CantidadPersonas")
                        .HasColumnType("int");

                    b.Property<int>("NumHabitacion")
                        .HasColumnType("int");

                    b.Property<int>("Tarifa")
                        .HasColumnType("int");

                    b.Property<string>("TipoHabitacion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdHabitacion");

                    b.ToTable("Habitaciones");
                });

            modelBuilder.Entity("Oblig2Web.Modelos.Huesped", b =>
                {
                    b.Property<int>("IdHuesped")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdHuesped"), 1L, 1);

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CorreoElec")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("date");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("NumDocumento")
                        .HasColumnType("int");

                    b.Property<int>("Telefono")
                        .HasColumnType("int");

                    b.Property<string>("TipoDocumento")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdHuesped");

                    b.ToTable("Huespedes");
                });

            modelBuilder.Entity("Oblig2Web.Modelos.Pago", b =>
                {
                    b.Property<int>("IdPago")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPago"), 1L, 1);

                    b.Property<DateTime>("FechaPago")
                        .HasColumnType("date");

                    b.Property<string>("MetodoPago")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Monto")
                        .HasColumnType("int");

                    b.Property<int>("ReservaId")
                        .HasColumnType("int");

                    b.HasKey("IdPago");

                    b.HasIndex("ReservaId")
                        .IsUnique();

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("Oblig2Web.Modelos.Reserva", b =>
                {
                    b.Property<int>("IdReserva")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdReserva"), 1L, 1);

                    b.Property<DateTime>("FechaFinal")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("date");

                    b.Property<DateTime>("FechaReserva")
                        .HasColumnType("date");

                    b.Property<int>("HabitacionId")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int?>("NumHabitacion")
                        .HasColumnType("int");

                    b.Property<int>("NumeroPersonas")
                        .HasColumnType("int");

                    b.Property<int>("TiempoEstadia")
                        .HasColumnType("int");

                    b.HasKey("IdReserva");

                    b.HasIndex("HabitacionId");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Reservas");
                });

            modelBuilder.Entity("Oblig2Web.Modelos.Usuario", b =>
                {
                    b.Property<int>("IdUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUsuario"), 1L, 1);

                    b.Property<string>("Contrasenia")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("CorreoElec")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HuespedId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUsuario");

                    b.HasIndex("HuespedId")
                        .IsUnique();

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Oblig2Web.Modelos.Pago", b =>
                {
                    b.HasOne("Oblig2Web.Modelos.Reserva", "Reserva")
                        .WithOne("Pago")
                        .HasForeignKey("Oblig2Web.Modelos.Pago", "ReservaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reserva");
                });

            modelBuilder.Entity("Oblig2Web.Modelos.Reserva", b =>
                {
                    b.HasOne("Oblig2Web.Modelos.Habitacion", "HabitacionElegida")
                        .WithMany("Reservas")
                        .HasForeignKey("HabitacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Oblig2Web.Modelos.Usuario", "Usuario")
                        .WithMany("Reservas")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HabitacionElegida");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Oblig2Web.Modelos.Usuario", b =>
                {
                    b.HasOne("Oblig2Web.Modelos.Huesped", "Huesped")
                        .WithOne("Usuario")
                        .HasForeignKey("Oblig2Web.Modelos.Usuario", "HuespedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Huesped");
                });

            modelBuilder.Entity("Oblig2Web.Modelos.Habitacion", b =>
                {
                    b.Navigation("Reservas");
                });

            modelBuilder.Entity("Oblig2Web.Modelos.Huesped", b =>
                {
                    b.Navigation("Usuario")
                        .IsRequired();
                });

            modelBuilder.Entity("Oblig2Web.Modelos.Reserva", b =>
                {
                    b.Navigation("Pago");
                });

            modelBuilder.Entity("Oblig2Web.Modelos.Usuario", b =>
                {
                    b.Navigation("Reservas");
                });
#pragma warning restore 612, 618
        }
    }
}