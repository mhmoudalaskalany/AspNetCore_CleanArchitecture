using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePermissionSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "ModifiedDate" },
                values: new object[] { "ADD", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "ModifiedDate" },
                values: new object[] { "EDIT", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Code", "ModifiedDate" },
                values: new object[] { "VIEW", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Code", "ModifiedDate" },
                values: new object[] { "DELETE", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("abcc43c2-f7b8-4d70-8c1e-81bc61cb4518"),
                column: "Password",
                value: "AKG0q+Sqbidh19i0YluYOE3wNvysuNRHyQp4Ceoj0YXQp2E9krSq/jGYi+IhS07/uA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "ModifiedDate" },
                values: new object[] { null, new DateTime(2023, 2, 19, 2, 54, 24, 266, DateTimeKind.Local).AddTicks(2496) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "ModifiedDate" },
                values: new object[] { null, new DateTime(2023, 2, 19, 2, 54, 24, 267, DateTimeKind.Local).AddTicks(5203) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Code", "ModifiedDate" },
                values: new object[] { null, new DateTime(2023, 2, 19, 2, 54, 24, 267, DateTimeKind.Local).AddTicks(5326) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Code", "ModifiedDate" },
                values: new object[] { null, new DateTime(2023, 2, 19, 2, 54, 24, 267, DateTimeKind.Local).AddTicks(5365) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("abcc43c2-f7b8-4d70-8c1e-81bc61cb4518"),
                column: "Password",
                value: "AHSRqviUhVnsr5xRcMvJP8IVTZ3JpUbVAsiuDeNs9XeRM9C7HyVARKMLVXKrNB2H+Q==");
        }
    }
}
