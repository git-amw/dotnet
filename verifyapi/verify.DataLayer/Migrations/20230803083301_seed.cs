using Microsoft.EntityFrameworkCore.Migrations;

namespace verify.DataLayer.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AadhaarData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AadhaarNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AadhaarData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PANData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PAN = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PANData", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AadhaarData",
                columns: new[] { "Id", "AadhaarNumber" },
                values: new object[,]
                {
                    { 1, "124692735693" },
                    { 2, "865196029806" },
                    { 3, "378926190808" },
                    { 4, "944879507126" },
                    { 5, "364866624600" }
                });

            migrationBuilder.InsertData(
                table: "PANData",
                columns: new[] { "Id", "PAN" },
                values: new object[,]
                {
                    { 1, "1343CA3FEC" },
                    { 2, "23CECC43EA" },
                    { 3, "FECD3D44FD" },
                    { 4, "AA4D1C1DAE" },
                    { 5, "24EB4AD3D2" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AadhaarData");

            migrationBuilder.DropTable(
                name: "PANData");
        }
    }
}
