using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackendCore.Data.Migrations
{
    public partial class AddInitialModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    CreatedById = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    Code = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    NameEn = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<long>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ModifiedById = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    NameEn = table.Column<string>(nullable: true),
                    NameAr = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    NationalId = table.Column<string>(nullable: true),
                    RoleId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Code", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "NameAr", "NameEn" },
                values: new object[,]
                {
                    { 1L, "Add", null, new DateTime(2020, 6, 27, 16, 3, 54, 204, DateTimeKind.Local).AddTicks(5132), false, null, new DateTime(2020, 6, 27, 16, 3, 54, 204, DateTimeKind.Local).AddTicks(5163), "اضافة", "Add" },
                    { 2L, "Edit", null, new DateTime(2020, 6, 27, 16, 3, 54, 204, DateTimeKind.Local).AddTicks(8889), false, null, new DateTime(2020, 6, 27, 16, 3, 54, 204, DateTimeKind.Local).AddTicks(8899), "تعديل", "Edit" },
                    { 3L, "View", null, new DateTime(2020, 6, 27, 16, 3, 54, 204, DateTimeKind.Local).AddTicks(8937), false, null, new DateTime(2020, 6, 27, 16, 3, 54, 204, DateTimeKind.Local).AddTicks(8938), "عرض", "View" },
                    { 4L, "Delete", null, new DateTime(2020, 6, 27, 16, 3, 54, 204, DateTimeKind.Local).AddTicks(8940), false, null, new DateTime(2020, 6, 27, 16, 3, 54, 204, DateTimeKind.Local).AddTicks(8941), "حذف", "Delete" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "NameAr", "NameEn" },
                values: new object[] { 1L, null, new DateTime(2020, 6, 27, 16, 3, 54, 191, DateTimeKind.Local).AddTicks(2916), false, null, new DateTime(2020, 6, 27, 16, 3, 54, 191, DateTimeKind.Local).AddTicks(7695), "مدير", "Admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "Email", "IsDeleted", "ModifiedById", "ModifiedDate", "NameAr", "NameEn", "NationalId", "Password", "Phone", "RoleId", "UserName" },
                values: new object[] { 1L, null, new DateTime(2020, 6, 27, 16, 3, 54, 193, DateTimeKind.Local).AddTicks(7647), "Admin@admin.com", false, null, new DateTime(2020, 6, 27, 16, 3, 54, 193, DateTimeKind.Local).AddTicks(7670), "مدير", "Admin", null, "ALZ9PvYMXnnwSioWiMEDkNTNf2Vzdc/c5A9ir4Bgl90PohQfSrnSQLqyM9vK6Q7IZw==", "01016670280", 1L, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Code",
                table: "Permissions",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
