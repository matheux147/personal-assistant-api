using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalAssistantApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarCampoPagoConta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Pago",
                table: "Contas",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pago",
                table: "Contas");
        }
    }
}
