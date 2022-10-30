using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindYourWayAPI.Migrations
{
    public partial class newTry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    FieldId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.FieldId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
