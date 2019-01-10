using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace PracaInz.Migrations
{
    public partial class addPresenceAndGrades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grade",
                columns: table => new
                {
                    GradeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseID = table.Column<int>(nullable: true),
                    EmployeerID = table.Column<int>(nullable: true),
                    StudentID = table.Column<int>(nullable: true),
                    Value = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grade", x => x.GradeID);
                    table.ForeignKey(
                        name: "FK_Grade_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grade_Employee_EmployeerID",
                        column: x => x.EmployeerID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grade_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Presence",
                columns: table => new
                {
                    PresenceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseID = table.Column<int>(nullable: true),
                    Data = table.Column<DateTime>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: true),
                    Godzina = table.Column<DateTime>(nullable: false),
                    IsPresent = table.Column<bool>(nullable: false),
                    StudentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presence", x => x.PresenceID);
                    table.ForeignKey(
                        name: "FK_Presence_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Presence_Employee_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "Employee",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Presence_Student_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Student",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grade_CourseID",
                table: "Grade",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_EmployeerID",
                table: "Grade",
                column: "EmployeerID");

            migrationBuilder.CreateIndex(
                name: "IX_Grade_StudentID",
                table: "Grade",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Presence_CourseID",
                table: "Presence",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_Presence_EmployeeID",
                table: "Presence",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_Presence_StudentID",
                table: "Presence",
                column: "StudentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grade");

            migrationBuilder.DropTable(
                name: "Presence");
        }
    }
}
