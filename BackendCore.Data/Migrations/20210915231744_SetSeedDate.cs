using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendCore.Data.Migrations
{
    public partial class SetSeedDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedDate", "ModifiedDate" },
                values: new object[] { new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("abcc43c2-f7b8-4d70-8c1e-81bc61cb4518"),
                columns: new[] { "CreatedDate", "ModifiedDate", "Password" },
                values: new object[] { new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "APvoUO9yVp4jzeyjSarvGu2KEDgNX1kC1x4IXk90e2qpig1quO8oSfySG0aCen+CiQ==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
