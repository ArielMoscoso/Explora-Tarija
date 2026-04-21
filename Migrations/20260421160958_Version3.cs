using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExploraTarija.Migrations
{
    /// <inheritdoc />
    public partial class Version3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Reservas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Estado",
                table: "Pago",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdReserva",
                table: "Pago",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Monto",
                table: "Pago",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ReservaIdReservas",
                table: "Pago",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pago_ReservaIdReservas",
                table: "Pago",
                column: "ReservaIdReservas");

            migrationBuilder.AddForeignKey(
                name: "FK_Pago_Reservas_ReservaIdReservas",
                table: "Pago",
                column: "ReservaIdReservas",
                principalTable: "Reservas",
                principalColumn: "IdReservas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pago_Reservas_ReservaIdReservas",
                table: "Pago");

            migrationBuilder.DropIndex(
                name: "IX_Pago_ReservaIdReservas",
                table: "Pago");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Reservas");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Pago");

            migrationBuilder.DropColumn(
                name: "IdReserva",
                table: "Pago");

            migrationBuilder.DropColumn(
                name: "Monto",
                table: "Pago");

            migrationBuilder.DropColumn(
                name: "ReservaIdReservas",
                table: "Pago");
        }
    }
}
