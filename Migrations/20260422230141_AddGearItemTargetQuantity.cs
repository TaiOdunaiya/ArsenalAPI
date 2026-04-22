using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ArsenalApi.Migrations
{
    /// <inheritdoc />
    public partial class AddGearItemTargetQuantity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TargetQuantity",
                table: "GearItems",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 1,
                column: "TargetQuantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 2,
                column: "TargetQuantity",
                value: 15);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 3,
                column: "TargetQuantity",
                value: 25);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 4,
                column: "TargetQuantity",
                value: 8);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 5,
                column: "TargetQuantity",
                value: 5);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 6,
                column: "TargetQuantity",
                value: 10);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 7,
                column: "TargetQuantity",
                value: 15);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 8,
                column: "TargetQuantity",
                value: 50);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 9,
                column: "TargetQuantity",
                value: 15);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 10,
                column: "TargetQuantity",
                value: 35);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 11,
                column: "TargetQuantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 12,
                column: "TargetQuantity",
                value: 10);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 13,
                column: "TargetQuantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 14,
                column: "TargetQuantity",
                value: 15);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 15,
                column: "TargetQuantity",
                value: 20);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 16,
                column: "TargetQuantity",
                value: 3);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 17,
                column: "TargetQuantity",
                value: 25);

            migrationBuilder.UpdateData(
                table: "GearItems",
                keyColumn: "Id",
                keyValue: 18,
                column: "TargetQuantity",
                value: 10);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetQuantity",
                table: "GearItems");
        }
    }
}
