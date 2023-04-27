using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vacation.Migrations
{
    /// <inheritdoc />
    public partial class Labb1WebbApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstMidName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "VacationTypes",
                columns: table => new
                {
                    VacationTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VacationTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationTypes", x => x.VacationTypeId);
                });

            migrationBuilder.CreateTable(
                name: "VacationLists",
                columns: table => new
                {
                    VacationListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VacCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FK_EmployeeId = table.Column<int>(type: "int", nullable: false),
                    FK_VacationTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationLists", x => x.VacationListId);
                    table.ForeignKey(
                        name: "FK_VacationLists_Employees_FK_EmployeeId",
                        column: x => x.FK_EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VacationLists_VacationTypes_FK_VacationTypeId",
                        column: x => x.FK_VacationTypeId,
                        principalTable: "VacationTypes",
                        principalColumn: "VacationTypeId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_VacationLists_FK_EmployeeId",
                table: "VacationLists",
                column: "FK_EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationLists_FK_VacationTypeId",
                table: "VacationLists",
                column: "FK_VacationTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacationLists");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "VacationTypes");
        }
    }
}
