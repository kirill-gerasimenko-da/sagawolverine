using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SagaHost.Migrations
{
    /// <inheritdoc />
    public partial class ExtendSagawithfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Saga",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Saga",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Saga");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Saga");
        }
    }
}
