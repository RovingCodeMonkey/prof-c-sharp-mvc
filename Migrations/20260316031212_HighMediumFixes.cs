using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class HighMediumFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "SalesPersons",
                type: "TEXT COLLATE NOCASE",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Manager",
                table: "SalesPersons",
                type: "TEXT COLLATE NOCASE",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT COLLATE NOCASE");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "SalesPersons",
                type: "TEXT",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Customers",
                type: "TEXT",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "SalesPersons",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT COLLATE NOCASE",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Manager",
                table: "SalesPersons",
                type: "TEXT COLLATE NOCASE",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT COLLATE NOCASE",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "SalesPersons",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Customers",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
