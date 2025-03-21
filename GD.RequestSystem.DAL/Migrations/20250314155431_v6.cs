using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GD.RequestSystem.DAL.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreaterPetitionID",
                table: "tblResultPetition");

            migrationBuilder.RenameColumn(
                name: "PetitionID",
                table: "tblResultPetition",
                newName: "PetitionId");

            migrationBuilder.RenameColumn(
                name: "urlDocumentResponse",
                table: "tblResultPetition",
                newName: "urlDocumentResponse");

            migrationBuilder.RenameColumn(
                name: "ResponseMessage",
                table: "tblResultPetition",
                newName: "MessageResponce");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "tblResultPetition",
                newName: "DateTimeResponse");

            migrationBuilder.RenameColumn(
                name: "ResultPetitionnID",
                table: "tblResultPetition",
                newName: "ResultPetitionID");

            migrationBuilder.RenameColumn(
                name: "urlDocument",
                table: "tblPetition",
                newName: "urlDocumentPetition");

            migrationBuilder.RenameColumn(
                name: "message",
                table: "tblPetition",
                newName: "MessagePetition");

            migrationBuilder.AddColumn<string>(
                name: "AreaKey",
                table: "tblAreas",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaKey",
                table: "tblAreas");

            migrationBuilder.RenameColumn(
                name: "PetitionId",
                table: "tblResultPetition",
                newName: "PetitionID");

            migrationBuilder.RenameColumn(
                name: "UrlDocumentResponse",
                table: "tblResultPetition",
                newName: "urlDocument");

            migrationBuilder.RenameColumn(
                name: "MessageResponce",
                table: "tblResultPetition",
                newName: "ResponseMessage");

            migrationBuilder.RenameColumn(
                name: "DateTimeResponse",
                table: "tblResultPetition",
                newName: "DateTime");

            migrationBuilder.RenameColumn(
                name: "ResultPetitionID",
                table: "tblResultPetition",
                newName: "ResultPetitionnID");

            migrationBuilder.RenameColumn(
                name: "urlDocumentPetition",
                table: "tblPetition",
                newName: "urlDocument");

            migrationBuilder.RenameColumn(
                name: "MessagePetition",
                table: "tblPetition",
                newName: "message");

            migrationBuilder.AddColumn<int>(
                name: "CreaterPetitionID",
                table: "tblResultPetition",
                type: "int",
                nullable: true);
        }
    }
}
