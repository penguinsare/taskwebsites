using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskWebsites.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.UniqueConstraint("AK_Categories_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "HomepageSnapshot",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PathToFileOnDisk = table.Column<string>(nullable: true),
                    UrlPath = table.Column<string>(nullable: true),
                    Filename = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomepageSnapshot", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebsiteCredentials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteCredentials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Websites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    URL = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<int>(nullable: true),
                    HomepageSnapshotId = table.Column<int>(nullable: true),
                    LoginId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Websites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Websites_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Websites_HomepageSnapshot_HomepageSnapshotId",
                        column: x => x.HomepageSnapshotId,
                        principalTable: "HomepageSnapshot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Websites_WebsiteCredentials_LoginId",
                        column: x => x.LoginId,
                        principalTable: "WebsiteCredentials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Automotive" },
                    { 24, "Transportation" },
                    { 23, "Government" },
                    { 22, "Telecommunications" },
                    { 21, "Technology" },
                    { 20, "Jewelry" },
                    { 19, "Retail" },
                    { 18, "Religion" },
                    { 17, "Raw materials" },
                    { 15, "Real estate" },
                    { 14, "Media" },
                    { 25, "Electronics" },
                    { 13, "Manufacturing" },
                    { 11, "Insurance" },
                    { 10, "Healthcare" },
                    { 9, "Food and beverage" },
                    { 8, "Financial" },
                    { 7, "Fashion" },
                    { 6, "Energy" },
                    { 5, "Engineering" },
                    { 4, "Education" },
                    { 3, "Consumer" },
                    { 2, "Banking" },
                    { 12, "Legal" },
                    { 26, "Non-profit" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Websites_CategoryId",
                table: "Websites",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Websites_HomepageSnapshotId",
                table: "Websites",
                column: "HomepageSnapshotId");

            migrationBuilder.CreateIndex(
                name: "IX_Websites_LoginId",
                table: "Websites",
                column: "LoginId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Websites");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "HomepageSnapshot");

            migrationBuilder.DropTable(
                name: "WebsiteCredentials");
        }
    }
}
