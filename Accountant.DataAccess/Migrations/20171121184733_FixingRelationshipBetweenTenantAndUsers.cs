using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Accountant.DataAccess.Migrations
{
    public partial class FixingRelationshipBetweenTenantAndUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserAccounts_TenantId",
                table: "UserAccounts",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAccounts_Tenants_TenantId",
                table: "UserAccounts",
                column: "TenantId",
                principalTable: "Tenants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAccounts_Tenants_TenantId",
                table: "UserAccounts");

            migrationBuilder.DropIndex(
                name: "IX_UserAccounts_TenantId",
                table: "UserAccounts");
        }
    }
}
