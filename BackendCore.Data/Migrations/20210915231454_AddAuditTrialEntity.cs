using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendCore.Data.Migrations
{
    public partial class AddAuditTrialEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditTrails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AffectedColumns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedById = table.Column<long>(type: "bigint", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedById = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrails", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 9, 16, 3, 14, 53, 633, DateTimeKind.Local).AddTicks(2186), new DateTime(2021, 9, 16, 3, 14, 53, 633, DateTimeKind.Local).AddTicks(2216) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 9, 16, 3, 14, 53, 633, DateTimeKind.Local).AddTicks(3750), new DateTime(2021, 9, 16, 3, 14, 53, 633, DateTimeKind.Local).AddTicks(3759) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 9, 16, 3, 14, 53, 633, DateTimeKind.Local).AddTicks(3761), new DateTime(2021, 9, 16, 3, 14, 53, 633, DateTimeKind.Local).AddTicks(3762) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 9, 16, 3, 14, 53, 633, DateTimeKind.Local).AddTicks(3764), new DateTime(2021, 9, 16, 3, 14, 53, 633, DateTimeKind.Local).AddTicks(3765) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 9, 16, 3, 14, 53, 624, DateTimeKind.Local).AddTicks(1030), new DateTime(2021, 9, 16, 3, 14, 53, 625, DateTimeKind.Local).AddTicks(2339) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("abcc43c2-f7b8-4d70-8c1e-81bc61cb4518"),
                columns: new[] { "CreatedDate", "ModifiedDate", "Password" },
                values: new object[] { new DateTime(2021, 9, 16, 3, 14, 53, 627, DateTimeKind.Local).AddTicks(610), new DateTime(2021, 9, 16, 3, 14, 53, 627, DateTimeKind.Local).AddTicks(622), "AL3fYgO/pCMCzBvJd/e2XMK2KfqyaSDHdaZPK97diaKI+6mlw/bpPGgqLHIUl5pGBw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrails");

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 9, 16, 2, 43, 19, 491, DateTimeKind.Local).AddTicks(9681), new DateTime(2021, 9, 16, 2, 43, 19, 491, DateTimeKind.Local).AddTicks(9694) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 9, 16, 2, 43, 19, 492, DateTimeKind.Local).AddTicks(465), new DateTime(2021, 9, 16, 2, 43, 19, 492, DateTimeKind.Local).AddTicks(469) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 9, 16, 2, 43, 19, 492, DateTimeKind.Local).AddTicks(471), new DateTime(2021, 9, 16, 2, 43, 19, 492, DateTimeKind.Local).AddTicks(472) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 9, 16, 2, 43, 19, 492, DateTimeKind.Local).AddTicks(474), new DateTime(2021, 9, 16, 2, 43, 19, 492, DateTimeKind.Local).AddTicks(474) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 9, 16, 2, 43, 19, 485, DateTimeKind.Local).AddTicks(4813), new DateTime(2021, 9, 16, 2, 43, 19, 486, DateTimeKind.Local).AddTicks(2748) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("abcc43c2-f7b8-4d70-8c1e-81bc61cb4518"),
                columns: new[] { "CreatedDate", "ModifiedDate", "Password" },
                values: new object[] { new DateTime(2021, 9, 16, 2, 43, 19, 487, DateTimeKind.Local).AddTicks(5725), new DateTime(2021, 9, 16, 2, 43, 19, 487, DateTimeKind.Local).AddTicks(5733), "AFM++3iAmDI90bIhQnC+0cZx+Dp7SSr9nj//zyESXEjqN3A02eOxEpKnumJdBjxErQ==" });
        }
    }
}
