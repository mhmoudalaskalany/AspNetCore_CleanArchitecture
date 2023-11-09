using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Template.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataForActionAndStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CssClass",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EntityName",
                table: "Statuses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "CreatedById", "CreatedDate", "ModifiedDate", "NameAr", "NameEn" },
                values: new object[] { "ADD", "1", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "إضافة", "Add" });

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "CreatedById", "CreatedDate", "ModifiedDate", "NameAr", "NameEn" },
                values: new object[] { "APPROVE", "1", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "موافقة", "Approve" });

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Code", "CreatedById", "CreatedDate", "ModifiedDate", "NameAr", "NameEn" },
                values: new object[] { "REJECT", "1", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "رفض", "Reject" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "CreatedById", "CreatedDate", "CssClass", "EntityName", "ModifiedDate", "NameAr", "NameEn" },
                values: new object[] { "NEW", "1", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "status-success", null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "جديدة", "New" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "CreatedById", "CreatedDate", "CssClass", "EntityName", "ModifiedDate", "NameAr", "NameEn" },
                values: new object[] { "PENDING", "1", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "status-info", null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "قيد الإنتظار", "Pending" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Code", "CreatedById", "CreatedDate", "CssClass", "EntityName", "ModifiedDate", "NameAr", "NameEn" },
                values: new object[] { "CLOSED", "1", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "status-warning", null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "مغلقة", "Closed" });

            migrationBuilder.InsertData(
                table: "Statuses",
                columns: new[] { "Id", "Code", "CreatedById", "CreatedDate", "CssClass", "EntityName", "IsDeleted", "ModifiedById", "ModifiedDate", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 4, "COMPLETED", "1", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "status-warning", null, false, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "مكتملة", "Completed" },
                    { 5, "REJECTED", "1", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "status-warning", null, false, null, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "مرفوض", "Rejected" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("abcc43c2-f7b8-4d70-8c1e-81bc61cb4518"),
                column: "Password",
                value: "ALcjzHUwEYmUesf3ZKrrmR9HZ8j+R/59i6sn6atBvRKbhpqtUhwiZYgm8/vULDb4Tg==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "CssClass",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "EntityName",
                table: "Statuses");

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "CreatedById", "CreatedDate", "ModifiedDate", "NameAr", "NameEn" },
                values: new object[] { "APPROVE", null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "موافقة", "Approve" });

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "CreatedById", "CreatedDate", "ModifiedDate", "NameAr", "NameEn" },
                values: new object[] { "REJECT", null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "رفض", "Reject" });

            migrationBuilder.UpdateData(
                table: "Actions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Code", "CreatedById", "CreatedDate", "ModifiedDate", "NameAr", "NameEn" },
                values: new object[] { "CLOSE", null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "إغلاق", "Close" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Code", "CreatedById", "CreatedDate", "ModifiedDate", "NameAr", "NameEn" },
                values: new object[] { "ACTIVE", null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "فعال", "Active" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Code", "CreatedById", "CreatedDate", "ModifiedDate", "NameAr", "NameEn" },
                values: new object[] { "IN-ACTIVE", null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "غير فعال", "InActive" });

            migrationBuilder.UpdateData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Code", "CreatedById", "CreatedDate", "ModifiedDate", "NameAr", "NameEn" },
                values: new object[] { "SUSPENDED", null, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "موقوف", "Suspended" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("abcc43c2-f7b8-4d70-8c1e-81bc61cb4518"),
                column: "Password",
                value: "AKG0q+Sqbidh19i0YluYOE3wNvysuNRHyQp4Ceoj0YXQp2E9krSq/jGYi+IhS07/uA==");
        }
    }
}
