using Microsoft.EntityFrameworkCore.Migrations;

namespace UserCenter.API.Migrations
{
    public partial class one : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(11)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(100)", nullable: true, defaultValue: "http://www.baidu.com/default.jpg")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.ID);
                    table.UniqueConstraint("AK_Students_Mobile", x => x.Mobile);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(11)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(32)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Introduce = table.Column<string>(type: "nvarchar(500)", nullable: true, defaultValue: "该老师很懒，还没添加自我介绍"),
                    Picture = table.Column<string>(type: "nvarchar(100)", nullable: true, defaultValue: "http://www.baidu.com/default.jpg")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.ID);
                    table.UniqueConstraint("AK_Teachers_Mobile", x => x.Mobile);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
