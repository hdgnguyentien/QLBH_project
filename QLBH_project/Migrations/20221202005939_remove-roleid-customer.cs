using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QLBH_project.Migrations
{
    public partial class removeroleidcustomer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Roles_rolesId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_rolesId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "rolesId",
                table: "Customers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "rolesId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_rolesId",
                table: "Customers",
                column: "rolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Roles_rolesId",
                table: "Customers",
                column: "rolesId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
