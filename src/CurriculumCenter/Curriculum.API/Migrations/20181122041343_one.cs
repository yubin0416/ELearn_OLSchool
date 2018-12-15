using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Curriculum.API.Migrations
{
    public partial class one : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Curriculums",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    PictureURL = table.Column<string>(type: "NVARCHAR(100)", nullable: false, defaultValue: ""),
                    Introduce = table.Column<string>(type: "NVARCHAR(500)", nullable: true, defaultValue: ""),
                    IsFree = table.Column<bool>(nullable: false, defaultValue: true),
                    CreateTime = table.Column<DateTime>(type: "Datetime", nullable: false, defaultValueSql: "Getdate()"),
                    EndTime = table.Column<DateTime>(type: "Datetime", nullable: false, defaultValueSql: "2999-1-1"),
                    Lecturer_TeacherID = table.Column<string>(type: "Nvarchar(36)", nullable: false),
                    Lecturer_Name = table.Column<string>(type: "Nvarchar(50)", nullable: false),
                    Lecturer_Picture = table.Column<string>(type: "Nvarchar(100)", nullable: false),
                    Lecturer_Introduce = table.Column<string>(type: "Nvarchar(500)", nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curriculums", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<string>(type: "Nvarchar(36)", nullable: false),
                    NickName = table.Column<string>(type: "Nvarchar(50)", nullable: false),
                    Picture = table.Column<string>(type: "Nvarchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Coursewares",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    Title = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    DownloadUrl = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    CurriculumID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coursewares", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Coursewares_Curriculums_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculums",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    SectionTitle = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    SectionIntroduce = table.Column<string>(type: "NVARCHAR(1000)", nullable: true, defaultValue: "SectionIntroduce"),
                    CreateTime = table.Column<DateTime>(type: "Datetime", nullable: false, defaultValueSql: "Getdate()"),
                    CurriculumID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sections_Curriculums_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculums",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR(36)", nullable: false),
                    CommentStar = table.Column<int>(nullable: false),
                    Context = table.Column<string>(type: "NVARCHAR(500)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false, computedColumnSql: "Getdate()"),
                    CurriculumID = table.Column<string>(nullable: false),
                    StudentID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Comments_Curriculums_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculums",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurriculumStudents",
                columns: table => new
                {
                    CurriculumID = table.Column<string>(nullable: false),
                    StudentID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurriculumStudents", x => new { x.CurriculumID, x.StudentID });
                    table.ForeignKey(
                        name: "FK_CurriculumStudents_Curriculums_CurriculumID",
                        column: x => x.CurriculumID,
                        principalTable: "Curriculums",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CurriculumStudents_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassHours",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    ClassHourTitle = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    ClassHourType = table.Column<int>(nullable: false, defaultValue: 1),
                    CreateDate = table.Column<DateTime>(type: "DATETIME", nullable: false, computedColumnSql: "GETDATE()"),
                    VedioUrl = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    VedioDuration = table.Column<string>(type: "nvarchar(8)", nullable: false, defaultValue: "00:00:00"),
                    IsFree = table.Column<bool>(nullable: false, defaultValue: true),
                    IsExperience = table.Column<bool>(nullable: false, defaultValue: true),
                    SectionID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassHours", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ClassHours_Sections_SectionID",
                        column: x => x.SectionID,
                        principalTable: "Sections",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassHours_SectionID",
                table: "ClassHours",
                column: "SectionID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CurriculumID",
                table: "Comments",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_StudentID",
                table: "Comments",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Coursewares_CurriculumID",
                table: "Coursewares",
                column: "CurriculumID");

            migrationBuilder.CreateIndex(
                name: "IX_Curriculums_Lecturer_TeacherID",
                table: "Curriculums",
                column: "Lecturer_TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_CurriculumStudents_StudentID",
                table: "CurriculumStudents",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_CurriculumID",
                table: "Sections",
                column: "CurriculumID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassHours");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Coursewares");

            migrationBuilder.DropTable(
                name: "CurriculumStudents");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Curriculums");
        }
    }
}
