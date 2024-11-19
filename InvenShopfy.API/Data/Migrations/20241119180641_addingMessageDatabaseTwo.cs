using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InvenShopfy.API.Migrations
{
    /// <inheritdoc />
    public partial class addingMessageDatabaseTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subtitle",
                table: "Message",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Message",
                newName: "MessageBody");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Message",
                newName: "Subtitle");

            migrationBuilder.RenameColumn(
                name: "MessageBody",
                table: "Message",
                newName: "Description");
        }
    }
}
