using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CVgrupp2Main.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
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
                name: "Erfarenheter",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Erfarenheter", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kompetenser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kompetenser", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Kontaktuppgifter",
                columns: table => new
                {
                    KontaktID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Telefonnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kontaktuppgifter", x => x.KontaktID);
                });

            migrationBuilder.CreateTable(
                name: "Meddelande",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Innehåll = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HarLästs = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meddelande", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Utbildningar",
                columns: table => new
                {
                    UtbildningID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utbildningar", x => x.UtbildningID);
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
                name: "Person",
                columns: table => new
                {
                    Användarnamn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Förnamn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Efternamn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lösenord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Privat = table.Column<bool>(type: "bit", nullable: false),
                    ProfilBild = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    kontaktID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Användarnamn);
                    table.ForeignKey(
                        name: "FK_Person_Kontaktuppgifter_kontaktID",
                        column: x => x.kontaktID,
                        principalTable: "Kontaktuppgifter",
                        principalColumn: "KontaktID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonErfarenheter",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    PersonID = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonErfarenheter", x => new { x.ID, x.PersonID });
                    table.ForeignKey(
                        name: "FK_PersonErfarenheter_Erfarenheter_ID",
                        column: x => x.ID,
                        principalTable: "Erfarenheter",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonErfarenheter_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "Användarnamn",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonKompetenser",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    PersonID = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonKompetenser", x => new { x.ID, x.PersonID });
                    table.ForeignKey(
                        name: "FK_PersonKompetenser_Kompetenser_ID",
                        column: x => x.ID,
                        principalTable: "Kompetenser",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonKompetenser_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "Användarnamn",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonMottagitMeddelande",
                columns: table => new
                {
                    Användarnamn = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    MeddelandeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonMottagitMeddelande", x => new { x.Användarnamn, x.MeddelandeID });
                    table.ForeignKey(
                        name: "FK_PersonMottagitMeddelande_Meddelande_MeddelandeID",
                        column: x => x.MeddelandeID,
                        principalTable: "Meddelande",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonMottagitMeddelande_Person_Användarnamn",
                        column: x => x.Användarnamn,
                        principalTable: "Person",
                        principalColumn: "Användarnamn",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonSkickatMeddelande",
                columns: table => new
                {
                    Användarnamn = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    MeddelandeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonSkickatMeddelande", x => new { x.Användarnamn, x.MeddelandeID });
                    table.ForeignKey(
                        name: "FK_PersonSkickatMeddelande_Meddelande_MeddelandeID",
                        column: x => x.MeddelandeID,
                        principalTable: "Meddelande",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonSkickatMeddelande_Person_Användarnamn",
                        column: x => x.Användarnamn,
                        principalTable: "Person",
                        principalColumn: "Användarnamn",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonUtbildningar",
                columns: table => new
                {
                    UtbildningID = table.Column<int>(type: "int", nullable: false),
                    PersonID = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonUtbildningar", x => new { x.PersonID, x.UtbildningID });
                    table.ForeignKey(
                        name: "FK_PersonUtbildningar_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "Användarnamn",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonUtbildningar_Utbildningar_UtbildningID",
                        column: x => x.UtbildningID,
                        principalTable: "Utbildningar",
                        principalColumn: "UtbildningID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projekt",
                columns: table => new
                {
                    ProjektID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Namn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beskrivning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Skapare = table.Column<string>(type: "nvarchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projekt", x => x.ProjektID);
                    table.ForeignKey(
                        name: "FK_Projekt_Person_Skapare",
                        column: x => x.Skapare,
                        principalTable: "Person",
                        principalColumn: "Användarnamn",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonProjekt",
                columns: table => new
                {
                    ProjektID = table.Column<int>(type: "int", maxLength: 255, nullable: false),
                    Medverkande = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonProjekt", x => new { x.ProjektID, x.Medverkande });
                    table.ForeignKey(
                        name: "FK_PersonProjekt_Person_Medverkande",
                        column: x => x.Medverkande,
                        principalTable: "Person",
                        principalColumn: "Användarnamn",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PersonProjekt_Projekt_ProjektID",
                        column: x => x.ProjektID,
                        principalTable: "Projekt",
                        principalColumn: "ProjektID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Erfarenheter",
                columns: new[] { "ID", "Beskrivning", "Titel" },
                values: new object[] { 1, "B", "A" });

            migrationBuilder.InsertData(
                table: "Kompetenser",
                columns: new[] { "ID", "Beskrivning", "Titel" },
                values: new object[] { 1, "A", "A" });

            migrationBuilder.InsertData(
                table: "Kontaktuppgifter",
                columns: new[] { "KontaktID", "Adress", "Email", "Telefonnummer" },
                values: new object[] { 1, "A123", "ABC@edunet.oru.se", "+46 123" });

            migrationBuilder.InsertData(
                table: "Meddelande",
                columns: new[] { "ID", "HarLästs", "Innehåll" },
                values: new object[] { 1, false, "A" });

            migrationBuilder.InsertData(
                table: "Utbildningar",
                columns: new[] { "UtbildningID", "Beskrivning", "Namn" },
                values: new object[,]
                {
                    { 1, "A", "A" },
                    { 2, "B", "B" }
                });

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "Användarnamn", "Efternamn", "Förnamn", "Lösenord", "Privat", "ProfilBild", "kontaktID" },
                values: new object[] { "A", "B", "A", "Abc123", false, null, 1 });

            migrationBuilder.InsertData(
                table: "PersonErfarenheter",
                columns: new[] { "ID", "PersonID" },
                values: new object[] { 1, "A" });

            migrationBuilder.InsertData(
                table: "PersonKompetenser",
                columns: new[] { "ID", "PersonID" },
                values: new object[] { 1, "A" });

            migrationBuilder.InsertData(
                table: "PersonMottagitMeddelande",
                columns: new[] { "Användarnamn", "MeddelandeID" },
                values: new object[] { "A", 1 });

            migrationBuilder.InsertData(
                table: "PersonSkickatMeddelande",
                columns: new[] { "Användarnamn", "MeddelandeID" },
                values: new object[] { "A", 1 });

            migrationBuilder.InsertData(
                table: "PersonUtbildningar",
                columns: new[] { "PersonID", "UtbildningID" },
                values: new object[] { "A", 1 });

            migrationBuilder.InsertData(
                table: "Projekt",
                columns: new[] { "ProjektID", "Beskrivning", "Namn", "Skapare" },
                values: new object[] { 1, "A", "A", "A" });

            migrationBuilder.InsertData(
                table: "PersonProjekt",
                columns: new[] { "Medverkande", "ProjektID" },
                values: new object[] { "A", 1 });

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
                name: "IX_Person_kontaktID",
                table: "Person",
                column: "kontaktID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonErfarenheter_PersonID",
                table: "PersonErfarenheter",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonKompetenser_PersonID",
                table: "PersonKompetenser",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonMottagitMeddelande_MeddelandeID",
                table: "PersonMottagitMeddelande",
                column: "MeddelandeID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonProjekt_Medverkande",
                table: "PersonProjekt",
                column: "Medverkande");

            migrationBuilder.CreateIndex(
                name: "IX_PersonSkickatMeddelande_MeddelandeID",
                table: "PersonSkickatMeddelande",
                column: "MeddelandeID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonUtbildningar_UtbildningID",
                table: "PersonUtbildningar",
                column: "UtbildningID");

            migrationBuilder.CreateIndex(
                name: "IX_Projekt_Skapare",
                table: "Projekt",
                column: "Skapare");
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
                name: "PersonErfarenheter");

            migrationBuilder.DropTable(
                name: "PersonKompetenser");

            migrationBuilder.DropTable(
                name: "PersonMottagitMeddelande");

            migrationBuilder.DropTable(
                name: "PersonProjekt");

            migrationBuilder.DropTable(
                name: "PersonSkickatMeddelande");

            migrationBuilder.DropTable(
                name: "PersonUtbildningar");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Erfarenheter");

            migrationBuilder.DropTable(
                name: "Kompetenser");

            migrationBuilder.DropTable(
                name: "Projekt");

            migrationBuilder.DropTable(
                name: "Meddelande");

            migrationBuilder.DropTable(
                name: "Utbildningar");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "Kontaktuppgifter");
        }
    }
}
