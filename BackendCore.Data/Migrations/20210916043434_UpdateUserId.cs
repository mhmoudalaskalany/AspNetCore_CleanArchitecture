using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendCore.Data.Migrations
{
    public partial class UpdateUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("abcc43c2-f7b8-4d70-8c1e-81bc61cb4518"),
                column: "Password",
                value: "AFj4thZS6h0t75r1K9h0dye8CIBVT37wJlNHnLgZXTYWKIupzWzAcWdDCrc1VQlM3w==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("abcc43c2-f7b8-4d70-8c1e-81bc61cb4518"),
                column: "Password",
                value: "APvoUO9yVp4jzeyjSarvGu2KEDgNX1kC1x4IXk90e2qpig1quO8oSfySG0aCen+CiQ==");
        }
    }
}
