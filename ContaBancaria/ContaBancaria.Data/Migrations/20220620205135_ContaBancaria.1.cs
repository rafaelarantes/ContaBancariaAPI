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
                name: "FILA_PROCESSAMENTO",
                columns: table => new
                {
                    GUID = table.Column<Guid>(nullable: false),
                    DADOS = table.Column<string>(type: "varchar(max)", nullable: false),
                    TIPO_COMANDO_FILA = table.Column<byte>(nullable: false),
                    SITUACAO = table.Column<byte>(nullable: false),
                    DATA_GERACAO = table.Column<DateTime>(type: "datetime", nullable: false),
                    DATA_PROCESSAMENTO = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FILA_PROCESSAMENTO", x => x.GUID);
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
                values: new object[] { new Guid("06d7dd04-0e57-4c26-8d68-a5fd6553dd16"), 11111, "Banco", 1 });

            migrationBuilder.InsertData(
                table: "USUARIO",
                columns: new[] { "GUID", "AUTORIZACAO", "LOGIN", "SENHA" },
                values: new object[,]
                {
                    { new Guid("b2a36205-ca9a-4d08-a760-9ccf7313c914"), "Conta", "conta", "7C4A8D09CA3762AF61E59520943DC26494F8941B" },
                    { new Guid("b06e9c97-3544-4ba3-aee2-98eb01008a85"), "Banco", "banco", "7C4A8D09CA3762AF61E59520943DC26494F8941B" },
                    { new Guid("d802c3b5-0e12-4759-a92d-c5c6c3859a38"), "BancoCentral", "central", "7C4A8D09CA3762AF61E59520943DC26494F8941B" },
                    { new Guid("cbf0d216-6904-4f5d-94fd-ee040a30c322"), "Adm", "adm", "7C4A8D09CA3762AF61E59520943DC26494F8941B" }
                });

            migrationBuilder.InsertData(
                table: "TAXA_BANCARIA",
                columns: new[] { "GUID", "DESCRICAO", "GUID_BANCO", "TIPO", "TIPO_VALOR", "VALOR" },
                values: new object[] { new Guid("69372eab-5107-49ea-94cd-897e91304e5f"), "1%", new Guid("06d7dd04-0e57-4c26-8d68-a5fd6553dd16"), (byte)2, (byte)1, 1m });

            migrationBuilder.InsertData(
                table: "TAXA_BANCARIA",
                columns: new[] { "GUID", "DESCRICAO", "GUID_BANCO", "TIPO", "TIPO_VALOR", "VALOR" },
                values: new object[] { new Guid("38963719-3780-468c-bcb7-a4d9248c7e15"), "R$ 4", new Guid("06d7dd04-0e57-4c26-8d68-a5fd6553dd16"), (byte)1, (byte)2, 4m });

            migrationBuilder.InsertData(
                table: "TAXA_BANCARIA",
                columns: new[] { "GUID", "DESCRICAO", "GUID_BANCO", "TIPO", "TIPO_VALOR", "VALOR" },
                values: new object[] { new Guid("f92b9799-dc12-436b-be12-25affb6366c3"), "R$ 1", new Guid("06d7dd04-0e57-4c26-8d68-a5fd6553dd16"), (byte)3, (byte)2, 1m });

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
                name: "FILA_PROCESSAMENTO");

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
