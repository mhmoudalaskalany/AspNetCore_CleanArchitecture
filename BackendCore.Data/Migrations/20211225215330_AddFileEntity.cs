using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendCore.Data.Migrations
{
    public partial class AddFileEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("abcc43c2-f7b8-4d70-8c1e-81bc61cb4518"),
                column: "Password",
                value: "AOuJ6y3b2DdXnldZKhmMg+1SGckHI9eRATfrH+oYQSKnia3whEbUM38QJgdLKvPcAg==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("abcc43c2-f7b8-4d70-8c1e-81bc61cb4518"),
                column: "Password",
                value: "ALpf52Z9a5aHszZjvLE/g5byB8g9/mljVT2VjQZs/9AxVz27RoGqKphB/6zQxnICuA==");
        }
    }
}
