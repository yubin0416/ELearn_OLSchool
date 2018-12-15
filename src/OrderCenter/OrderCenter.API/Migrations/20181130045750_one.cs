using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderCenter.API.Migrations
{
    public partial class one : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ID = table.Column<string>(type: "varchar(36)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    CurriculumID = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    CurriculumTitle = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "Datetime", nullable: false, defaultValueSql: "getdate()"),
                    TransationID = table.Column<string>(type: "Nvarchar(36)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CurriculumPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    DiscountsPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    ActualPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    OrderStatus = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserID",
                table: "Orders",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
