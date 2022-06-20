using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ContaBancaria.Data.Migrations
{
    public partial class ContaBancaria2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TAXA_BANCARIA",
                keyColumn: "GUID",
                keyValue: new Guid("57574877-3562-4d83-ac69-9e40aadfa5d9"));

            migrationBuilder.DeleteData(
                table: "TAXA_BANCARIA",
                keyColumn: "GUID",
                keyValue: new Guid("9490fa28-66a8-4bf1-b8c1-1e80f83d14fd"));

            migrationBuilder.DeleteData(
                table: "TAXA_BANCARIA",
                keyColumn: "GUID",
                keyValue: new Guid("fc056d11-ed57-4b2a-a371-f66fe38ba496"));

            migrationBuilder.DeleteData(
                table: "USUARIO",
                keyColumn: "GUID",
                keyValue: new Guid("56b5df15-b978-4c17-86ee-09cc4047e417"));

            migrationBuilder.DeleteData(
                table: "USUARIO",
                keyColumn: "GUID",
                keyValue: new Guid("644e3a32-7d67-4bd4-b08e-e21338b9f7c6"));

            migrationBuilder.DeleteData(
                table: "USUARIO",
                keyColumn: "GUID",
                keyValue: new Guid("9315a43d-bc8d-4a45-8050-641a033983df"));

            migrationBuilder.DeleteData(
                table: "USUARIO",
                keyColumn: "GUID",
                keyValue: new Guid("9c0ff412-3496-431c-a89c-22982030cfa4"));

            migrationBuilder.DeleteData(
                table: "BANCO",
                keyColumn: "GUID",
                keyValue: new Guid("20ef055c-2f7a-4995-83ee-4555207e0608"));

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

            migrationBuilder.InsertData(
                table: "BANCO",
                columns: new[] { "GUID", "AGENCIA", "NOME", "NUMERO" },
                values: new object[] { new Guid("f1efd866-934e-4337-bd2c-756f0885139b"), 11111, "Banco", 1 });

            migrationBuilder.InsertData(
                table: "USUARIO",
                columns: new[] { "GUID", "AUTORIZACAO", "LOGIN", "SENHA" },
                values: new object[,]
                {
                    { new Guid("b856bcdb-c0b1-460a-b4bb-bcdf5b7f1cb1"), "Conta", "conta", "7C4A8D09CA3762AF61E59520943DC26494F8941B" },
                    { new Guid("9dacd231-9a24-4ba8-841f-d588214a2c82"), "Banco", "banco", "7C4A8D09CA3762AF61E59520943DC26494F8941B" },
                    { new Guid("b53fc76d-5ac1-405c-b53f-9c54bbf4e988"), "BancoCentral", "central", "7C4A8D09CA3762AF61E59520943DC26494F8941B" },
                    { new Guid("ccdc7bcf-ab59-4e93-8491-0ab11857156b"), "Adm", "adm", "7C4A8D09CA3762AF61E59520943DC26494F8941B" }
                });

            migrationBuilder.InsertData(
                table: "TAXA_BANCARIA",
                columns: new[] { "GUID", "DESCRICAO", "GUID_BANCO", "TIPO", "TIPO_VALOR", "VALOR" },
                values: new object[] { new Guid("a2b30a0f-a97e-4f3d-b6ac-88a4378e3cf1"), "1%", new Guid("f1efd866-934e-4337-bd2c-756f0885139b"), (byte)2, (byte)1, 1m });

            migrationBuilder.InsertData(
                table: "TAXA_BANCARIA",
                columns: new[] { "GUID", "DESCRICAO", "GUID_BANCO", "TIPO", "TIPO_VALOR", "VALOR" },
                values: new object[] { new Guid("1fba310f-2137-436e-9917-9c91071a8f20"), "R$ 4", new Guid("f1efd866-934e-4337-bd2c-756f0885139b"), (byte)1, (byte)2, 4m });

            migrationBuilder.InsertData(
                table: "TAXA_BANCARIA",
                columns: new[] { "GUID", "DESCRICAO", "GUID_BANCO", "TIPO", "TIPO_VALOR", "VALOR" },
                values: new object[] { new Guid("057e97d1-c753-4d9a-b056-c619e96ea9e0"), "R$ 1", new Guid("f1efd866-934e-4337-bd2c-756f0885139b"), (byte)3, (byte)2, 1m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FILA_PROCESSAMENTO");

            migrationBuilder.DeleteData(
                table: "TAXA_BANCARIA",
                keyColumn: "GUID",
                keyValue: new Guid("057e97d1-c753-4d9a-b056-c619e96ea9e0"));

            migrationBuilder.DeleteData(
                table: "TAXA_BANCARIA",
                keyColumn: "GUID",
                keyValue: new Guid("1fba310f-2137-436e-9917-9c91071a8f20"));

            migrationBuilder.DeleteData(
                table: "TAXA_BANCARIA",
                keyColumn: "GUID",
                keyValue: new Guid("a2b30a0f-a97e-4f3d-b6ac-88a4378e3cf1"));

            migrationBuilder.DeleteData(
                table: "USUARIO",
                keyColumn: "GUID",
                keyValue: new Guid("9dacd231-9a24-4ba8-841f-d588214a2c82"));

            migrationBuilder.DeleteData(
                table: "USUARIO",
                keyColumn: "GUID",
                keyValue: new Guid("b53fc76d-5ac1-405c-b53f-9c54bbf4e988"));

            migrationBuilder.DeleteData(
                table: "USUARIO",
                keyColumn: "GUID",
                keyValue: new Guid("b856bcdb-c0b1-460a-b4bb-bcdf5b7f1cb1"));

            migrationBuilder.DeleteData(
                table: "USUARIO",
                keyColumn: "GUID",
                keyValue: new Guid("ccdc7bcf-ab59-4e93-8491-0ab11857156b"));

            migrationBuilder.DeleteData(
                table: "BANCO",
                keyColumn: "GUID",
                keyValue: new Guid("f1efd866-934e-4337-bd2c-756f0885139b"));

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
        }
    }
}
