using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitecture.Infraestructure.Migrations
{
    public partial class CreacionEntidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CleanArch");

            migrationBuilder.CreateTable(
                name: "Estudiante",
                schema: "CleanArch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaInscripcion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiante", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Instructor",
                schema: "CleanArch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaContratacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departamento",
                schema: "CleanArch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Presupuesto = table.Column<decimal>(type: "money", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstructorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departamento_Instructor_InstructorId",
                        column: x => x.InstructorId,
                        principalSchema: "CleanArch",
                        principalTable: "Instructor",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OficinaAsignacion",
                schema: "CleanArch",
                columns: table => new
                {
                    InstructorID = table.Column<int>(type: "int", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OficinaAsignacion", x => x.InstructorID);
                    table.ForeignKey(
                        name: "FK_OficinaAsignacion_Instructor_InstructorID",
                        column: x => x.InstructorID,
                        principalSchema: "CleanArch",
                        principalTable: "Instructor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                schema: "CleanArch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Creditos = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Curso_Departamento_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalSchema: "CleanArch",
                        principalTable: "Departamento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CursoAsignacion",
                schema: "CleanArch",
                columns: table => new
                {
                    InstructorId = table.Column<int>(type: "int", nullable: false),
                    CursoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CursoAsignacion", x => new { x.CursoId, x.InstructorId });
                    table.ForeignKey(
                        name: "FK_CursoAsignacion_Curso_CursoId",
                        column: x => x.CursoId,
                        principalSchema: "CleanArch",
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CursoAsignacion_Instructor_InstructorId",
                        column: x => x.InstructorId,
                        principalSchema: "CleanArch",
                        principalTable: "Instructor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inscripcion",
                schema: "CleanArch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CursoId = table.Column<int>(type: "int", nullable: false),
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    Grado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscripcion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inscripcion_Curso_CursoId",
                        column: x => x.CursoId,
                        principalSchema: "CleanArch",
                        principalTable: "Curso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inscripcion_Estudiante_EstudianteId",
                        column: x => x.EstudianteId,
                        principalSchema: "CleanArch",
                        principalTable: "Estudiante",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Curso_DepartamentoId",
                schema: "CleanArch",
                table: "Curso",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_CursoAsignacion_InstructorId",
                schema: "CleanArch",
                table: "CursoAsignacion",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Departamento_InstructorId",
                schema: "CleanArch",
                table: "Departamento",
                column: "InstructorId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripcion_CursoId",
                schema: "CleanArch",
                table: "Inscripcion",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Inscripcion_EstudianteId",
                schema: "CleanArch",
                table: "Inscripcion",
                column: "EstudianteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CursoAsignacion",
                schema: "CleanArch");

            migrationBuilder.DropTable(
                name: "Inscripcion",
                schema: "CleanArch");

            migrationBuilder.DropTable(
                name: "OficinaAsignacion",
                schema: "CleanArch");

            migrationBuilder.DropTable(
                name: "Curso",
                schema: "CleanArch");

            migrationBuilder.DropTable(
                name: "Estudiante",
                schema: "CleanArch");

            migrationBuilder.DropTable(
                name: "Departamento",
                schema: "CleanArch");

            migrationBuilder.DropTable(
                name: "Instructor",
                schema: "CleanArch");
        }
    }
}
