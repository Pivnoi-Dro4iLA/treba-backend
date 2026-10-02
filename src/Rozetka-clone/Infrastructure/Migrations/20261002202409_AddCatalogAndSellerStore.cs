using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCatalogAndSellerStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "sellers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyName = table.Column<string>(
                        type: "character varying(250)",
                        maxLength: 250,
                        nullable: false),
                    TaxNumber = table.Column<string>(
                        type: "character varying(100)",
                        maxLength: 100,
                        nullable: false),
                    Description = table.Column<string>(
                        type: "character varying(2000)",
                        maxLength: 2000,
                        nullable: true),
                    Phone = table.Column<string>(
                        type: "character varying(50)",
                        maxLength: 50,
                        nullable: true),
                    Email = table.Column<string>(
                        type: "character varying(320)",
                        maxLength: 320,
                        nullable: true),
                    Status = table.Column<string>(
                        type: "character varying(50)",
                        maxLength: 50,
                        nullable: false),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false),
                    UpdatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sellers", x => x.Id);

                    table.ForeignKey(
                        name: "FK_sellers_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "stores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(
                        type: "character varying(250)",
                        maxLength: 250,
                        nullable: false),
                    Slug = table.Column<string>(
                        type: "character varying(250)",
                        maxLength: 250,
                        nullable: false),
                    Description = table.Column<string>(
                        type: "character varying(2000)",
                        maxLength: 2000,
                        nullable: true),
                    LogoUrl = table.Column<string>(
                        type: "character varying(1000)",
                        maxLength: 1000,
                        nullable: true),
                    IsActive = table.Column<bool>(
                        type: "boolean",
                        nullable: false),
                    CreatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false),
                    UpdatedAt = table.Column<DateTime>(
                        type: "timestamp with time zone",
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stores", x => x.Id);

                    table.ForeignKey(
                        name: "FK_stores_sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "sellers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_sellers_TaxNumber",
                table: "sellers",
                column: "TaxNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_sellers_UserId",
                table: "sellers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_stores_SellerId",
                table: "stores",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_stores_Slug",
                table: "stores",
                column: "Slug",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_products_stores_StoreId",
                table: "products",
                column: "StoreId",
                principalTable: "stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_stores_StoreId",
                table: "products");

            migrationBuilder.DropTable(
                name: "stores");

            migrationBuilder.DropTable(
                name: "sellers");
        }
    }
}
