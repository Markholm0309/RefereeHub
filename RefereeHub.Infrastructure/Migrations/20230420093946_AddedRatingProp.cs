﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefereeHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRatingProp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Reports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Referees",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Referees");
        }
    }
}
