using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pandorga_Admin.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Professor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Especializacao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Turma",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeTurma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ano = table.Column<int>(type: "int", nullable: false),
                    Turno = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfessorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turma", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Turma_Professor_ProfessorID",
                        column: x => x.ProfessorID,
                        principalTable: "Professor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Aluno",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NomeResponsavel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContatoResponsavel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TurmaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluno", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Aluno_Turma_TurmaID",
                        column: x => x.TurmaID,
                        principalTable: "Turma",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Sala",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacidade = table.Column<int>(type: "int", nullable: false),
                    TurmaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sala", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sala_Turma_TurmaID",
                        column: x => x.TurmaID,
                        principalTable: "Turma",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Evento",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeEvento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TurmaID = table.Column<int>(type: "int", nullable: true),
                    SalaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evento", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Evento_Sala_SalaID",
                        column: x => x.SalaID,
                        principalTable: "Sala",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Evento_Turma_TurmaID",
                        column: x => x.TurmaID,
                        principalTable: "Turma",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluno_TurmaID",
                table: "Aluno",
                column: "TurmaID");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_SalaID",
                table: "Evento",
                column: "SalaID");

            migrationBuilder.CreateIndex(
                name: "IX_Evento_TurmaID",
                table: "Evento",
                column: "TurmaID");

            migrationBuilder.CreateIndex(
                name: "IX_Sala_TurmaID",
                table: "Sala",
                column: "TurmaID",
                unique: true,
                filter: "[TurmaID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Turma_ProfessorID",
                table: "Turma",
                column: "ProfessorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aluno");

            migrationBuilder.DropTable(
                name: "Evento");

            migrationBuilder.DropTable(
                name: "Sala");

            migrationBuilder.DropTable(
                name: "Turma");

            migrationBuilder.DropTable(
                name: "Professor");
        }
    }
}
