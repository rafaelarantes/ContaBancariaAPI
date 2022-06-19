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
                name: "USUARIO",
                columns: table => new
                {
                    GUID = table.Column<Guid>(nullable: false),
                    LOGIN = table.Column<string>(type: "varchar(100)", nullable: true),
                    SENHA = table.Column<string>(type: "varchar(100)", nullable: true),
                    AUTORIZACAO = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.GUID);
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
                    TIPO = table.Column<byte>(nullable: true),
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
                    TIPO_TAXA_BANCARIA = table.Column<byte>(nullable: true)
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

            migrationBuilder.InsertData(
                table: "BANCO",
                columns: new[] { "GUID", "AGENCIA", "NOME", "NUMERO" },
                values: new object[] { new Guid("20ef055c-2f7a-4995-83ee-4555207e0608"), 11111, "Banco", 1 });

            migrationBuilder.InsertData(
                table: "USUARIO",
                columns: new[] { "GUID", "AUTORIZACAO", "LOGIN", "SENHA" },
                values: new object[,]
                {
                    { new Guid("9c0ff412-3496-431c-a89c-22982030cfa4"), "Conta", "conta", "7C4A8D09CA3762AF61E59520943DC26494F8941B" },
                    { new Guid("644e3a32-7d67-4bd4-b08e-e21338b9f7c6"), "Banco", "banco", "7C4A8D09CA3762AF61E59520943DC26494F8941B" },
                    { new Guid("9315a43d-bc8d-4a45-8050-641a033983df"), "BancoCentral", "central", "7C4A8D09CA3762AF61E59520943DC26494F8941B" },
                    { new Guid("56b5df15-b978-4c17-86ee-09cc4047e417"), "Adm", "adm", "7C4A8D09CA3762AF61E59520943DC26494F8941B" }
                });

            migrationBuilder.InsertData(
                table: "TAXA_BANCARIA",
                columns: new[] { "GUID", "DESCRICAO", "GUID_BANCO", "TIPO", "TIPO_VALOR", "VALOR" },
                values: new object[] { new Guid("9490fa28-66a8-4bf1-b8c1-1e80f83d14fd"), "1%", new Guid("20ef055c-2f7a-4995-83ee-4555207e0608"), (byte)2, (byte)1, 1m });

            migrationBuilder.InsertData(
                table: "TAXA_BANCARIA",
                columns: new[] { "GUID", "DESCRICAO", "GUID_BANCO", "TIPO", "TIPO_VALOR", "VALOR" },
                values: new object[] { new Guid("fc056d11-ed57-4b2a-a371-f66fe38ba496"), "R$ 4", new Guid("20ef055c-2f7a-4995-83ee-4555207e0608"), (byte)1, (byte)2, 4m });

            migrationBuilder.InsertData(
                table: "TAXA_BANCARIA",
                columns: new[] { "GUID", "DESCRICAO", "GUID_BANCO", "TIPO", "TIPO_VALOR", "VALOR" },
                values: new object[] { new Guid("57574877-3562-4d83-ac69-9e40aadfa5d9"), "R$ 1", new Guid("20ef055c-2f7a-4995-83ee-4555207e0608"), (byte)3, (byte)2, 1m });

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
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "CONTA");

            migrationBuilder.DropTable(
                name: "BANCO");
        }
    }
}
