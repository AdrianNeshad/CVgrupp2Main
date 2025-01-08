using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVgrupp2Main.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPersonData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Kontaktuppgifter_kontaktID",
                table: "Person");

            migrationBuilder.DeleteData(
                table: "Utbildningar",
                keyColumn: "UtbildningID",
                keyValue: 2);

            migrationBuilder.RenameColumn(
                name: "kontaktID",
                table: "Person",
                newName: "KontaktID");

            migrationBuilder.RenameIndex(
                name: "IX_Person_kontaktID",
                table: "Person",
                newName: "IX_Person_KontaktID");

            migrationBuilder.UpdateData(
                table: "Erfarenheter",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Beskrivning", "Titel" },
                values: new object[] { "Systemutvecklare", "Polismyndigheten CTO" });

            migrationBuilder.UpdateData(
                table: "Kompetenser",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Beskrivning", "Titel" },
                values: new object[] { "Tjuvstarta o sånt", "Mecka med bilar" });

            migrationBuilder.UpdateData(
                table: "Kontaktuppgifter",
                keyColumn: "KontaktID",
                keyValue: 1,
                column: "Adress",
                value: "Brickebacken 123");

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "Användarnamn",
                keyValue: "A",
                columns: new[] { "Efternamn", "Förnamn" },
                values: new object[] { "Bolognese", "Pasta" });

            migrationBuilder.UpdateData(
                table: "Projekt",
                keyColumn: "ProjektID",
                keyValue: 1,
                columns: new[] { "Beskrivning", "Namn" },
                values: new object[] { "Programmera .NET", ".NET" });

            migrationBuilder.UpdateData(
                table: "Utbildningar",
                keyColumn: "UtbildningID",
                keyValue: 1,
                columns: new[] { "Beskrivning", "Namn" },
                values: new object[] { "Becknarkunskap", "Vivalla University" });

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Kontaktuppgifter_KontaktID",
                table: "Person",
                column: "KontaktID",
                principalTable: "Kontaktuppgifter",
                principalColumn: "KontaktID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_Kontaktuppgifter_KontaktID",
                table: "Person");

            migrationBuilder.RenameColumn(
                name: "KontaktID",
                table: "Person",
                newName: "kontaktID");

            migrationBuilder.RenameIndex(
                name: "IX_Person_KontaktID",
                table: "Person",
                newName: "IX_Person_kontaktID");

            migrationBuilder.UpdateData(
                table: "Erfarenheter",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Beskrivning", "Titel" },
                values: new object[] { "B", "A" });

            migrationBuilder.UpdateData(
                table: "Kompetenser",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "Beskrivning", "Titel" },
                values: new object[] { "A", "A" });

            migrationBuilder.UpdateData(
                table: "Kontaktuppgifter",
                keyColumn: "KontaktID",
                keyValue: 1,
                column: "Adress",
                value: "A123");

            migrationBuilder.UpdateData(
                table: "Person",
                keyColumn: "Användarnamn",
                keyValue: "A",
                columns: new[] { "Efternamn", "Förnamn" },
                values: new object[] { "B", "A" });

            migrationBuilder.UpdateData(
                table: "Projekt",
                keyColumn: "ProjektID",
                keyValue: 1,
                columns: new[] { "Beskrivning", "Namn" },
                values: new object[] { "A", "A" });

            migrationBuilder.UpdateData(
                table: "Utbildningar",
                keyColumn: "UtbildningID",
                keyValue: 1,
                columns: new[] { "Beskrivning", "Namn" },
                values: new object[] { "A", "A" });

            migrationBuilder.InsertData(
                table: "Utbildningar",
                columns: new[] { "UtbildningID", "Beskrivning", "Namn" },
                values: new object[] { 2, "B", "B" });

            migrationBuilder.AddForeignKey(
                name: "FK_Person_Kontaktuppgifter_kontaktID",
                table: "Person",
                column: "kontaktID",
                principalTable: "Kontaktuppgifter",
                principalColumn: "KontaktID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
