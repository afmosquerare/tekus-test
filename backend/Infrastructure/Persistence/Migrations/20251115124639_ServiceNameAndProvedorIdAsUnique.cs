using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ServiceNameAndProvedorIdAsUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Providers_ProviderId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_Name",
                table: "Services");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProviderId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_Id_Name",
                table: "Services",
                columns: new[] { "Id", "Name" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Providers_ProviderId",
                table: "Services",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Providers_ProviderId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_Id_Name",
                table: "Services");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProviderId",
                table: "Services",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Name",
                table: "Services",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Providers_ProviderId",
                table: "Services",
                column: "ProviderId",
                principalTable: "Providers",
                principalColumn: "Id");
        }
    }
}
