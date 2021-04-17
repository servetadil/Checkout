﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Checkout.PaymentGateway.Infrastructure.Migrations
{
    public partial class CheckoutPaymentDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Merchants",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MerchantID = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ApiKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentID = table.Column<Guid>(type: "uniqueidentifier", maxLength: 36, nullable: false),
                    OrderID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    MerchantId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CardNumber = table.Column<long>(type: "bigint", nullable: true),
                    Card_CvvCode = table.Column<int>(type: "int", nullable: true),
                    CardName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryMonth = table.Column<int>(type: "int", nullable: true),
                    ExpiryYear = table.Column<int>(type: "int", nullable: true),
                    Card_CardNetwork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFutureTransaction = table.Column<bool>(type: "bit", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Merchants_MerchantId",
                        column: x => x.MerchantId,
                        principalSchema: "dbo",
                        principalTable: "Merchants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_MerchantId",
                schema: "dbo",
                table: "Payments",
                column: "MerchantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Merchants",
                schema: "dbo");
        }
    }
}
