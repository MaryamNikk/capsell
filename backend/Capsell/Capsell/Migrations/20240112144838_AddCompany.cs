using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Capsell.Migrations
{
    public partial class AddCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Companies_AspNetUsers_UserId",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.DropIndex(
                name: "IX_Companies_UserId",
                table: "Companies");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "company");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "company",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "PhotoUrl",
                table: "company",
                newName: "photoUrl");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "company",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "LicenseNumber",
                table: "company",
                newName: "licenseNumber");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "company",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "company",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "photoUrl",
                table: "company",
                type: "nvarchar(999)",
                maxLength: 999,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_company",
                table: "company",
                column: "id");

            migrationBuilder.CreateTable(
                name: "companiesProduct",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<long>(type: "bigint", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    photoUrl = table.Column<string>(type: "nvarchar(999)", maxLength: 999, nullable: true),
                    companyId = table.Column<int>(type: "int", nullable: false),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companiesProduct", x => x.id);
                    table.ForeignKey(
                        name: "FK_companiesProduct_company_companyId",
                        column: x => x.companyId,
                        principalTable: "company",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopOrder",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyProducts = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    totalPrice = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    companyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopOrder", x => x.id);
                    table.ForeignKey(
                        name: "FK_ShopOrder_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopOrder_company_companyId",
                        column: x => x.companyId,
                        principalTable: "company",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "shopCart",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyProductId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    companyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_shopCart", x => x.id);
                    table.ForeignKey(
                        name: "FK_shopCart_AspNetUsers_userId",
                        column: x => x.userId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shopCart_companiesProduct_CompanyProductId",
                        column: x => x.CompanyProductId,
                        principalTable: "companiesProduct",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shopCart_company_companyId",
                        column: x => x.companyId,
                        principalTable: "company",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_company_userId",
                table: "company",
                column: "userId",
                unique: true,
                filter: "[userId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_companiesProduct_companyId",
                table: "companiesProduct",
                column: "companyId");

            migrationBuilder.CreateIndex(
                name: "IX_shopCart_companyId",
                table: "shopCart",
                column: "companyId");

            migrationBuilder.CreateIndex(
                name: "IX_shopCart_CompanyProductId",
                table: "shopCart",
                column: "CompanyProductId");

            migrationBuilder.CreateIndex(
                name: "IX_shopCart_userId",
                table: "shopCart",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopOrder_companyId",
                table: "ShopOrder",
                column: "companyId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopOrder_userId",
                table: "ShopOrder",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_company_AspNetUsers_userId",
                table: "company",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_company_AspNetUsers_userId",
                table: "company");

            migrationBuilder.DropTable(
                name: "shopCart");

            migrationBuilder.DropTable(
                name: "ShopOrder");

            migrationBuilder.DropTable(
                name: "companiesProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_company",
                table: "company");

            migrationBuilder.DropIndex(
                name: "IX_company_userId",
                table: "company");

            migrationBuilder.RenameTable(
                name: "company",
                newName: "Companies");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Companies",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "photoUrl",
                table: "Companies",
                newName: "PhotoUrl");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Companies",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "licenseNumber",
                table: "Companies",
                newName: "LicenseNumber");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Companies",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Companies",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "PhotoUrl",
                table: "Companies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(999)",
                oldMaxLength: 999,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_UserId",
                table: "Companies",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Companies_AspNetUsers_UserId",
                table: "Companies",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
