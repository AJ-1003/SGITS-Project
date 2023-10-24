using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGITS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedFamilyMemberFullNameColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMembers_Houses_HouseId",
                table: "FamilyMembers");

            migrationBuilder.AlterColumn<Guid>(
                name: "HouseId",
                table: "FamilyMembers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "FamilyMembers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMembers_Houses_HouseId",
                table: "FamilyMembers",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FamilyMembers_Houses_HouseId",
                table: "FamilyMembers");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "FamilyMembers");

            migrationBuilder.AlterColumn<Guid>(
                name: "HouseId",
                table: "FamilyMembers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FamilyMembers_Houses_HouseId",
                table: "FamilyMembers",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
