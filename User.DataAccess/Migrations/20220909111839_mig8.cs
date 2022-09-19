using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManager.DataAccess.Migrations
{
    public partial class mig8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_Users_usersId",
                table: "UserAddress");

            migrationBuilder.DropIndex(
                name: "IX_UserAddress_usersId",
                table: "UserAddress");

            migrationBuilder.DropColumn(
                name: "usersId",
                table: "UserAddress");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_UserId",
                table: "UserAddress",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_Users_UserId",
                table: "UserAddress",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_Users_UserId",
                table: "UserAddress");

            migrationBuilder.DropIndex(
                name: "IX_UserAddress_UserId",
                table: "UserAddress");

            migrationBuilder.AddColumn<int>(
                name: "usersId",
                table: "UserAddress",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_usersId",
                table: "UserAddress",
                column: "usersId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_Users_usersId",
                table: "UserAddress",
                column: "usersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
