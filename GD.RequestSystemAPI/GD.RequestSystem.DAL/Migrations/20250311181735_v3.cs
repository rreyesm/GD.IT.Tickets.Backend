using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GD.RequestSystem.DAL.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblResultPetition",
                columns: table => new
                {
                    ResultPetitionnID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PetitionID = table.Column<int>(type: "int", nullable: false),
                    ResponseMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreaterPetitionID = table.Column<int>(type: "int", nullable: true),
                    userResponceID = table.Column<int>(type: "int", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    urlDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreaterID = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdaterID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<int>(type: "int", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblResultPetition", x => x.ResultPetitionnID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblResponcePetition",
                columns: table => new
                {
                    ResponcePetitionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreaterID = table.Column<int>(type: "int", nullable: true),
                    CreaterPetitionID = table.Column<int>(type: "int", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeleterID = table.Column<int>(type: "int", nullable: true),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastUpdaterID = table.Column<int>(type: "int", nullable: true),
                    PetitionID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    responseMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    urlDocument = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userResponceID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblResponcePetition", x => x.ResponcePetitionID);
                });
        }
    }
}
