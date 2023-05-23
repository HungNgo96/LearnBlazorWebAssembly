using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class migration_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufactureDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Declaration",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerRights = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Declaration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Declaration_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QA",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QA", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QA_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6bf36d39-d5ec-4df9-86b9-73bfc1af039d", null, "Viewer", "VIEWER" },
                    { "6fa365de-f0c4-48ac-9ce2-2f0d178b4082", null, "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImageUrl", "ManufactureDate", "Name", "Price", "Supplier" },
                values: new object[,]
                {
                    { new Guid("0102f709-1dd7-40de-af3d-23598c6bbd1f"), "https://ih1.redbubble.net/image.1062161969.4889/mug,travel,x1000,center-pad,1000x1000,f8f8f8.u2.jpg", new DateTime(2020, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Travel Mug", 11.0, "Code Maze" },
                    { new Guid("2d3c2abe-85ec-4d1e-9fef-9b4bfea5f459"), "https://ih1.redbubble.net/image.1063329780.4889/mwo,x1000,ipad_2_snap-pad,1000x1000,f8f8f8.u2.jpg", new DateTime(2020, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "iPad Case & Skin", 45.0, "Code Maze" },
                    { new Guid("488aaa0e-aa7e-4820-b4e9-5715f0e5186e"), "https://ih1.redbubble.net/image.1062161956.4889/icr,iphone_11_soft,back,a,x1000-pad,1000x1000,f8f8f8.u2.jpg", new DateTime(2020, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "iPhone Case & Cover", 25.0, "Code Maze" },
                    { new Guid("4e693871-788d-4db4-89e5-dd7678db975e"), "https://ih1.redbubble.net/image.1062161956.4889/icr,samsung_galaxy_s10_snap,back,a,x1000-pad,1000x1000,f8f8f8.1u2.jpg", new DateTime(2019, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Case & Skin for Samsung Galaxy", 35.0, "Code Maze" },
                    { new Guid("54b2f952-b63e-4cad-8b38-c09955fe4c62"), "https://ih1.redbubble.net/image.1063364659.4889/ssrco,mhoodiez,mens,101010:01c5ca27c6,front,square_three_quarter,1000x1000-bg,f8f8f8.u2.jpg", new DateTime(2021, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Fitted Scoop T-Shirt", 40.0, "Code Maze" },
                    { new Guid("83e0aa87-158f-4e5f-a8f7-e5a98d13e3a5"), "https://ih1.redbubble.net/image.1063364659.4889/ra,fitted_scoop,x2000,101010:01c5ca27c6,front-c,160,143,1000,1000-bg,f8f8f8.u2.jpg", new DateTime(2013, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zipped Hoodie", 55.0, "Code Maze" },
                    { new Guid("ac7de2dc-049c-4328-ab06-6cde3ebe8aa7"), "https://ih1.redbubble.net/image.1063377597.4889/ur,mug_lifestyle,square,1000x1000.u2.jpg", new DateTime(2016, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Classic Mug", 22.0, "Code Maze" },
                    { new Guid("b47d4c3c-3e29-49b9-b6be-28e5ee4625be"), "https://ih1.redbubble.net/image.1063364659.4889/ssrco,mhoodie,mens,101010:01c5ca27c6,front,square_three_quarter,x1000-bg,f8f8f8.1u2.jpg", new DateTime(2015, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pullover Hoodie", 30.0, "Code Maze" },
                    { new Guid("d1f22836-6342-480a-be2f-035eeb010fd0"), "https://ih1.redbubble.net/image.1062161997.4889/clkc,bamboo,white,1000x1000-bg,f8f8f8.u2.jpg", new DateTime(2020, 7, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wall Clock", 25.0, "Code Maze" },
                    { new Guid("d26384cb-64b9-4aca-acb0-4ebb8fc53ba2"), "https://ih1.redbubble.net/image.1063364659.4889/ra,vneck,x1900,101010:01c5ca27c6,front-c,160,70,1000,1000-bg,f8f8f8.u2.jpg", new DateTime(2020, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Code Maze Logo T-Shirt", 20.0, "Code Maze" }
                });

            migrationBuilder.InsertData(
                table: "Declaration",
                columns: new[] { "Id", "CustomerRights", "Model", "Origin", "ProductId" },
                values: new object[] { new Guid("13feebbc-ab65-4e37-aa39-fcc2ed5e5015"), "All customer rights guaranteed under consumer protection law.", "Case & Skin for Samsung Galaxy G324 149 x 70.4 x 7.8 mm", "USA", new Guid("4e693871-788d-4db4-89e5-dd7678db975e") });

            migrationBuilder.InsertData(
                table: "QA",
                columns: new[] { "Id", "Answer", "ProductId", "Question", "User" },
                values: new object[,]
                {
                    { new Guid("06579037-943b-4ce5-8dd6-39f34ad49329"), "Hello Brigit. You can order it online on our web shop.", new Guid("4e693871-788d-4db4-89e5-dd7678db975e"), "How can I get this product", "Brigit Fansey" },
                    { new Guid("94402f9d-f280-4a7d-9c95-13e430065cee"), "Hello Mick. Yes, there is a two year guarantee for it.", new Guid("4e693871-788d-4db4-89e5-dd7678db975e"), "Is there a guarantee for this product", "Mick Simons" }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "Comment", "ProductId", "Rate", "User" },
                values: new object[,]
                {
                    { new Guid("b4031733-83a6-4d7c-b995-a3a0c0a35c39"), "Great product. Fits my phone perfectly.", new Guid("4e693871-788d-4db4-89e5-dd7678db975e"), 5, "Tim Malock" },
                    { new Guid("b88bc5c2-660d-4604-ba92-69abf546e881"), "It could be better, I am not that satisfied.", new Guid("4e693871-788d-4db4-89e5-dd7678db975e"), 3, "John Mining" },
                    { new Guid("f43017fd-1a65-4ad1-8610-ec4154a21c87"), "I use it all the time. Excellent stuff.", new Guid("4e693871-788d-4db4-89e5-dd7678db975e"), 4, "Ana Swan" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Declaration_ProductId",
                table: "Declaration",
                column: "ProductId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QA_ProductId",
                table: "QA",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ProductId",
                table: "Review",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Declaration");

            migrationBuilder.DropTable(
                name: "QA");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
