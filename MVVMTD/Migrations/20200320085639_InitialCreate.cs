using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MvvmTD.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(maxLength: 80, nullable: false),
                    Prenom = table.Column<string>(maxLength: 80, nullable: false),
                    Email = table.Column<string>(maxLength: 1600, nullable: true),
                    Telephone = table.Column<string>(maxLength: 20, nullable: true),
                    Adresse = table.Column<string>(maxLength: 255, nullable: true),
                    Anniversaire = table.Column<DateTime>(nullable: false),
                    Num_mobile = table.Column<string>(maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nom = table.Column<string>(maxLength: 80, nullable: false),
                    Prenom = table.Column<string>(maxLength: 80, nullable: false),
                    Email = table.Column<string>(maxLength: 1600, nullable: true),
                    Telephone = table.Column<string>(maxLength: 20, nullable: true),
                    Adresse = table.Column<string>(maxLength: 255, nullable: true),
                    Num_client = table.Column<int>(nullable: false),
                    Societe = table.Column<string>(maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amis");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
