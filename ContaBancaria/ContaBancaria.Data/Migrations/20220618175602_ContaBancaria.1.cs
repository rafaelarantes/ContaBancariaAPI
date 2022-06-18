using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContaBancaria.Data.Migrations
{
    public partial class ContaBancaria1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BANCO",
                columns: table => new
                {
                    GUID = table.Column<Guid>(nullable: false),
                    NOME = table.Column<string>(type: "varchar(100)", nullable: false),
                    NUMERO = table.Column<int>(type: "int", nullable: false),
                    AGENCIA = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BANCO", x => x.GUID);
                });

            migrationBuilder.CreateTable(
                name: "CONTA",
                columns: table => new
                {
                    GUID = table.Column<Guid>(nullable: false),
                    NUMERO = table.Column<long>(type: "bigint", nullable: false),
                    GUID_BANCO = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTA", x => x.GUID);
                    table.ForeignKey(
                        name: "FK_CONTA_BANCO_GUID_BANCO",
                        column: x => x.GUID_BANCO,
                        principalTable: "BANCO",
                        principalColumn: "GUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TAXA_BANCARIA",
                columns: table => new
                {
                    GUID = table.Column<Guid>(nullable: false),
                    VALOR = table.Column<decimal>(type: "numeric", nullable: false),
                    TIPO_VALOR = table.Column<byte>(nullable: false),
                    TIPO = table.Column<byte>(nullable: false),
                    DESCRICAO = table.Column<string>(type: "varchar(100)", nullable: true),
                    GUID_BANCO = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TAXA_BANCARIA", x => x.GUID);
                    table.ForeignKey(
                        name: "FK_TAXA_BANCARIA_BANCO_GUID_BANCO",
                        column: x => x.GUID_BANCO,
                        principalTable: "BANCO",
                        principalColumn: "GUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EXTRATO_CONTA",
                columns: table => new
                {
                    GUID = table.Column<Guid>(nullable: false),
                    DATA_OPERACAO = table.Column<DateTime>(type: "datetime", nullable: false),
                    TIPO_OPERACAO = table.Column<byte>(nullable: false),
                    VALOR = table.Column<decimal>(type: "money", nullable: false),
                    GUID_CONTA = table.Column<Guid>(nullable: false),
                    GUID_CONTA_ORIGEM = table.Column<Guid>(nullable: false),
                    TIPO_TAXA_BANCARIA = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EXTRATO_CONTA", x => x.GUID);
                    table.ForeignKey(
                        name: "FK_EXTRATO_CONTA_CONTA_GUID_CONTA",
                        column: x => x.GUID_CONTA,
                        principalTable: "CONTA",
                        principalColumn: "GUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CONTA_GUID_BANCO",
                table: "CONTA",
                column: "GUID_BANCO");

            migrationBuilder.CreateIndex(
                name: "IX_EXTRATO_CONTA_GUID_CONTA",
                table: "EXTRATO_CONTA",
                column: "GUID_CONTA");

            migrationBuilder.CreateIndex(
                name: "IX_TAXA_BANCARIA_GUID_BANCO",
                table: "TAXA_BANCARIA",
                column: "GUID_BANCO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EXTRATO_CONTA");

            migrationBuilder.DropTable(
                name: "TAXA_BANCARIA");

            migrationBuilder.DropTable(
                name: "CONTA");

            migrationBuilder.DropTable(
                name: "BANCO");
        }
    }
}
