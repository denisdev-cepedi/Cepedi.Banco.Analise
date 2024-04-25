using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cepedi.Banco.Analise.Dados.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PessoaCredito",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    CartaoCredito = table.Column<bool>(type: "bit", nullable: false),
                    ChequeEspecial = table.Column<bool>(type: "bit", nullable: false),
                    LimiteCredito = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaCredito", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PessoaCredito_Cpf",
                table: "PessoaCredito",
                column: "Cpf",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PessoaCredito");
        }
    }
}
