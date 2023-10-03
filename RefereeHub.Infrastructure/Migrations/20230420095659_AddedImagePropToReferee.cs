using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefereeHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedImagePropToReferee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Referees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Referees");
        }
    }
}
