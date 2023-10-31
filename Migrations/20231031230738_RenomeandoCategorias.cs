using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Rest_C_.Migrations
{
    /// <inheritdoc />
    public partial class RenomeandoCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Produtos_ProdutoId",
                table: "Tag");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "Tag",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Produtos_ProdutoId",
                table: "Tag",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tag_Produtos_ProdutoId",
                table: "Tag");

            migrationBuilder.AlterColumn<int>(
                name: "ProdutoId",
                table: "Tag",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_Produtos_ProdutoId",
                table: "Tag",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id");
        }
    }
}
