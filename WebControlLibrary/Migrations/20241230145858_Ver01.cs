using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebControlLibrary.Migrations
{
    /// <inheritdoc />
    public partial class Ver01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PublishedYear = table.Column<int>(type: "int", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Books__3DE0C2279156CD0F", x => x.BookID);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    ReaderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Readers__8E67A581499F9ACA", x => x.ReaderID);
                });

            migrationBuilder.CreateTable(
                name: "Borrowing",
                columns: table => new
                {
                    BorrowID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReaderID = table.Column<int>(type: "int", nullable: true),
                    BookID = table.Column<int>(type: "int", nullable: true),
                    BorrowDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Borrowin__4295F85F7F37564F", x => x.BorrowID);
                    table.ForeignKey(
                        name: "FK__Borrowing__BookI__5070F446",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK__Borrowing__Reade__4F7CD00D",
                        column: x => x.ReaderID,
                        principalTable: "Readers",
                        principalColumn: "ReaderID");
                });

            migrationBuilder.CreateTable(
                name: "Returning",
                columns: table => new
                {
                    ReturnID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BorrowID = table.Column<int>(type: "int", nullable: true),
                    ReturnDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LateFee = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Returnin__F445E988277AFD62", x => x.ReturnID);
                    table.ForeignKey(
                        name: "FK__Returning__Borro__5441852A",
                        column: x => x.BorrowID,
                        principalTable: "Borrowing",
                        principalColumn: "BorrowID");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__Books__447D36EA9250F971",
                table: "Books",
                column: "ISBN",
                unique: true,
                filter: "[ISBN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_BookID",
                table: "Borrowing",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_Borrowing_ReaderID",
                table: "Borrowing",
                column: "ReaderID");

            migrationBuilder.CreateIndex(
                name: "IX_Returning_BorrowID",
                table: "Returning",
                column: "BorrowID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Returning");

            migrationBuilder.DropTable(
                name: "Borrowing");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Readers");
        }
    }
}
