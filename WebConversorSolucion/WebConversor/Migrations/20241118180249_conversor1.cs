﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebConversor.Migrations
{
    /// <inheritdoc />
    public partial class conversor1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coins", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Img = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FromCoin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromAmount = table.Column<double>(type: "float", nullable: false),
                    ToCoin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToAmount = table.Column<double>(type: "float", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExchangeHistory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Coins",
                columns: new[] { "Id", "Name", "ShortName", "Symbol" },
                values: new object[,]
                {
                    { 1, "D�lar Estadounidense", "USD", "USD" },
                    { 2, "Euro", "EUR", "EUR" },
                    { 3, "Yen Japon�s", "YEN", "JPY" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FechaNacimiento", "Img", "LastName", "Name", "Password" },
                values: new object[,]
                {
                    { 1, "asda@gmail.com", null, "dd", "Gomez", "Julian", "ddd" },
                    { 2, "ggrg2@gmail.com", null, "ff", "Garcia", "Manuel", "fff" }
                });

            migrationBuilder.InsertData(
                table: "ExchangeHistory",
                columns: new[] { "Id", "Date", "FromAmount", "FromCoin", "ToAmount", "ToCoin", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2004, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 76.0, "EUR", 2.0, "USD", 1 },
<<<<<<<< HEAD:WebConversorSolucion/WebConversor/Migrations/20241118180249_conversor1.cs
                    { 2, new DateTime(2024, 11, 18, 19, 2, 48, 620, DateTimeKind.Local).AddTicks(5939), 20.0, "USD", 16.0, "EUR", 2 },
========
                    { 2, new DateTime(2024, 11, 18, 21, 4, 26, 194, DateTimeKind.Local).AddTicks(5129), 20.0, "USD", 16.0, "EUR", 2 },
>>>>>>>> 07e4d72d36dda98246e51f8e37be999d057d7207:WebConversorSolucion/WebConversor/Migrations/20241118200427_primera-migracion.cs
                    { 3, new DateTime(2007, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 45.0, "USD", 120.0, "PLN", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeHistory_UserId",
                table: "ExchangeHistory",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coins");

            migrationBuilder.DropTable(
                name: "ExchangeHistory");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
