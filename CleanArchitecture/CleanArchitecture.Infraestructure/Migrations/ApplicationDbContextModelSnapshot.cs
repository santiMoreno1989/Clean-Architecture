﻿// <auto-generated />
using System;
using CleanArchitecture.Infraestructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CleanArchitecture.Infraestructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("CleanArch")
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Curso", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Creditos")
                        .HasColumnType("int");

                    b.Property<int>("DepartamentoId")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DepartamentoId");

                    b.ToTable("Curso", "CleanArch");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CursoAsignacion", b =>
                {
                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int");

                    b.HasKey("CursoId", "InstructorId");

                    b.HasIndex("InstructorId");

                    b.ToTable("CursoAsignacion", "CleanArch");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Departamento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int?>("InstructorId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.Property<decimal>("Presupuesto")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.ToTable("Departamento", "CleanArch");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Estudiante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime>("FechaInscripcion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Estudiante", "CleanArch");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Inscripcion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CursoId")
                        .HasColumnType("int");

                    b.Property<int>("EstudianteId")
                        .HasColumnType("int");

                    b.Property<int>("Grado")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CursoId");

                    b.HasIndex("EstudianteId");

                    b.ToTable("Inscripcion", "CleanArch");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaContratacion")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Instructor", "CleanArch");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.OficinaAsignacion", b =>
                {
                    b.Property<int>("InstructorID")
                        .HasColumnType("int");

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("InstructorID");

                    b.ToTable("OficinaAsignacion", "CleanArch");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Curso", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Departamento", "Departamento")
                        .WithMany("Curso")
                        .HasForeignKey("DepartamentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Departamento");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.CursoAsignacion", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Curso", "Curso")
                        .WithMany("CursoAsignaciones")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanArchitecture.Domain.Entities.Instructor", "Instructor")
                        .WithMany("CursoAsignaciones")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Departamento", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Instructor", "Instructor")
                        .WithMany()
                        .HasForeignKey("InstructorId");

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Inscripcion", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Curso", "Curso")
                        .WithMany("Inscripciones")
                        .HasForeignKey("CursoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CleanArchitecture.Domain.Entities.Estudiante", "Estudiante")
                        .WithMany("Inscripciones")
                        .HasForeignKey("EstudianteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Curso");

                    b.Navigation("Estudiante");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.OficinaAsignacion", b =>
                {
                    b.HasOne("CleanArchitecture.Domain.Entities.Instructor", "Instructor")
                        .WithOne("OficinaAsignacion")
                        .HasForeignKey("CleanArchitecture.Domain.Entities.OficinaAsignacion", "InstructorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Instructor");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Curso", b =>
                {
                    b.Navigation("CursoAsignaciones");

                    b.Navigation("Inscripciones");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Departamento", b =>
                {
                    b.Navigation("Curso");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Estudiante", b =>
                {
                    b.Navigation("Inscripciones");
                });

            modelBuilder.Entity("CleanArchitecture.Domain.Entities.Instructor", b =>
                {
                    b.Navigation("CursoAsignaciones");

                    b.Navigation("OficinaAsignacion")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
