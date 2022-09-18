using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employees.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblDesignations",
                columns: table => new
                {
                    DesignationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Designation = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDesignations", x => x.DesignationId);
                });

            migrationBuilder.CreateTable(
                name: "TblEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Doj = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DesignationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblEmployees_TblDesignations_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "TblDesignations",
                        principalColumn: "DesignationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblEmployees_DesignationId",
                table: "TblEmployees",
                column: "DesignationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblEmployees");

            migrationBuilder.DropTable(
                name: "TblDesignations");
        }
    }
}
