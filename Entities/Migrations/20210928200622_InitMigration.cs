using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Entities.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    IdClient = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomClient = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.IdClient);
                });

            migrationBuilder.CreateTable(
                name: "Famille",
                columns: table => new
                {
                    IdFamille = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomFamille = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Famille", x => x.IdFamille);
                });

            migrationBuilder.CreateTable(
                name: "Fonction",
                columns: table => new
                {
                    IdFonction = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomFonction = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fonction", x => x.IdFonction);
                });

            migrationBuilder.CreateTable(
                name: "Marque",
                columns: table => new
                {
                    IdMarque = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomMarque = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marque", x => x.IdMarque);
                });

            migrationBuilder.CreateTable(
                name: "Origine",
                columns: table => new
                {
                    IdOrigine = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomOrigine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Origine", x => x.IdOrigine);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    IdService = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomService = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.IdService);
                });

            migrationBuilder.CreateTable(
                name: "Source",
                columns: table => new
                {
                    IdSource = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomSource = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Source", x => x.IdSource);
                });

            migrationBuilder.CreateTable(
                name: "SubstitutionProduit",
                columns: table => new
                {
                    IdSubstitutionProduit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProduitPrincipal = table.Column<int>(type: "int", nullable: false),
                    IdProduitSubstitution = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubstitutionProduit", x => x.IdSubstitutionProduit);
                });

            migrationBuilder.CreateTable(
                name: "TypeBCI",
                columns: table => new
                {
                    IdTypeBCI = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomTypeBCI = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeBCI", x => x.IdTypeBCI);
                });

            migrationBuilder.CreateTable(
                name: "TypeCatalogue",
                columns: table => new
                {
                    IdTypeCatalogue = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomTypeCatalogue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeCatalogue", x => x.IdTypeCatalogue);
                });

            migrationBuilder.CreateTable(
                name: "TypeTicket",
                columns: table => new
                {
                    IdTypeTicket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomTypeTicket = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTicket", x => x.IdTypeTicket);
                });

            migrationBuilder.CreateTable(
                name: "Unite",
                columns: table => new
                {
                    IdUnite = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomUnite = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unite", x => x.IdUnite);
                });

            migrationBuilder.CreateTable(
                name: "ZoneStockage",
                columns: table => new
                {
                    IdZoneStockage = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomZoneStockage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneStockage", x => x.IdZoneStockage);
                });

            migrationBuilder.CreateTable(
                name: "SousFamille",
                columns: table => new
                {
                    IdSousFamille = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomSousFamille = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdFamille = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SousFamille", x => x.IdSousFamille);
                    table.ForeignKey(
                        name: "FK_SousFamille_Famille_IdFamille",
                        column: x => x.IdFamille,
                        principalTable: "Famille",
                        principalColumn: "IdFamille",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Personnel",
                columns: table => new
                {
                    IdPersonnel = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomPersonnel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrenomPersonnel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelPersonnel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdFonction = table.Column<int>(type: "int", nullable: false),
                    IdService = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personnel", x => x.IdPersonnel);
                    table.ForeignKey(
                        name: "FK_Personnel_Fonction_IdFonction",
                        column: x => x.IdFonction,
                        principalTable: "Fonction",
                        principalColumn: "IdFonction",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Personnel_Service_IdService",
                        column: x => x.IdService,
                        principalTable: "Service",
                        principalColumn: "IdService",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Fournisseur",
                columns: table => new
                {
                    IdFournisseur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomFournisseur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdSource = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fournisseur", x => x.IdFournisseur);
                    table.ForeignKey(
                        name: "FK_Fournisseur_Source_IdSource",
                        column: x => x.IdSource,
                        principalTable: "Source",
                        principalColumn: "IdSource",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Catalogue",
                columns: table => new
                {
                    IdCatalogue = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomCatalogue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdClientService = table.Column<int>(type: "int", nullable: false),
                    IdTypeCatalogue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogue", x => x.IdCatalogue);
                    table.ForeignKey(
                        name: "FK_Catalogue_TypeCatalogue_IdTypeCatalogue",
                        column: x => x.IdTypeCatalogue,
                        principalTable: "TypeCatalogue",
                        principalColumn: "IdTypeCatalogue",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Produit",
                columns: table => new
                {
                    IdProduit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomProduit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodeEAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrixAchat = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PrixVente = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    PoidsTotal = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    StockMinimalUnite = table.Column<int>(type: "int", nullable: false),
                    StockMinimalPoids = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    QteReassort = table.Column<int>(type: "int", nullable: false),
                    IsEnKilogramme = table.Column<bool>(type: "bit", nullable: false),
                    UVC = table.Column<int>(type: "int", nullable: false),
                    ShortTime = table.Column<int>(type: "int", nullable: false),
                    ImageProduit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdFamille = table.Column<int>(type: "int", nullable: false),
                    IdSousFamille = table.Column<int>(type: "int", nullable: false),
                    IdZoneStockage = table.Column<int>(type: "int", nullable: false),
                    IdSource = table.Column<int>(type: "int", nullable: false),
                    IdOrigine = table.Column<int>(type: "int", nullable: false),
                    IdMarque = table.Column<int>(type: "int", nullable: false),
                    IdUnite = table.Column<int>(type: "int", nullable: false),
                    IdUniteFacturation = table.Column<int>(type: "int", nullable: false),
                    IdUniteGestionStock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produit", x => x.IdProduit);
                    table.ForeignKey(
                        name: "FK_Produit_Famille_IdFamille",
                        column: x => x.IdFamille,
                        principalTable: "Famille",
                        principalColumn: "IdFamille",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produit_Source_IdSource",
                        column: x => x.IdSource,
                        principalTable: "Source",
                        principalColumn: "IdSource",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produit_SousFamille_IdSousFamille",
                        column: x => x.IdSousFamille,
                        principalTable: "SousFamille",
                        principalColumn: "IdSousFamille",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Produit_Unite_IdUnite",
                        column: x => x.IdUnite,
                        principalTable: "Unite",
                        principalColumn: "IdUnite",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Produit_ZoneStockage_IdZoneStockage",
                        column: x => x.IdZoneStockage,
                        principalTable: "ZoneStockage",
                        principalColumn: "IdZoneStockage",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BCI",
                columns: table => new
                {
                    IdBCI = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroBCI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateBCI = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateValidationBCI = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsValider = table.Column<bool>(type: "bit", nullable: false),
                    IsAnnuler = table.Column<bool>(type: "bit", nullable: false),
                    ObsBCI = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdPersonnel = table.Column<int>(type: "int", nullable: false),
                    IdTypeBCI = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BCI", x => x.IdBCI);
                    table.ForeignKey(
                        name: "FK_BCI_Personnel_IdPersonnel",
                        column: x => x.IdPersonnel,
                        principalTable: "Personnel",
                        principalColumn: "IdPersonnel",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BCI_TypeBCI_IdTypeBCI",
                        column: x => x.IdTypeBCI,
                        principalTable: "TypeBCI",
                        principalColumn: "IdTypeBCI",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ticket",
                columns: table => new
                {
                    IdTicket = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCommande = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateValidation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateLivraison = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObservationTicket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsTicketValider = table.Column<bool>(type: "bit", nullable: false),
                    IdService = table.Column<int>(type: "int", nullable: false),
                    IdClient = table.Column<int>(type: "int", nullable: false),
                    IdFournisseur = table.Column<int>(type: "int", nullable: false),
                    IdPersonnel = table.Column<int>(type: "int", nullable: false),
                    IdValideur = table.Column<int>(type: "int", nullable: false),
                    IdTypeTicket = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.IdTicket);
                    table.ForeignKey(
                        name: "FK_Ticket_Personnel_IdPersonnel",
                        column: x => x.IdPersonnel,
                        principalTable: "Personnel",
                        principalColumn: "IdPersonnel",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ticket_TypeTicket_IdTypeTicket",
                        column: x => x.IdTypeTicket,
                        principalTable: "TypeTicket",
                        principalColumn: "IdTypeTicket",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommandeFournisseur",
                columns: table => new
                {
                    IdCommandeFournisseur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroCmdeFsseur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCmdeFsseur = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ObsCmdeFsseur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdFournisseur = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommandeFournisseur", x => x.IdCommandeFournisseur);
                    table.ForeignKey(
                        name: "FK_CommandeFournisseur_Fournisseur_IdFournisseur",
                        column: x => x.IdFournisseur,
                        principalTable: "Fournisseur",
                        principalColumn: "IdFournisseur",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatalogueProduit",
                columns: table => new
                {
                    IdCatalogueProduit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdProduit = table.Column<int>(type: "int", nullable: false),
                    IdCatalogue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatalogueProduit", x => x.IdCatalogueProduit);
                    table.ForeignKey(
                        name: "FK_CatalogueProduit_Catalogue_IdCatalogue",
                        column: x => x.IdCatalogue,
                        principalTable: "Catalogue",
                        principalColumn: "IdCatalogue",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatalogueProduit_Produit_IdProduit",
                        column: x => x.IdProduit,
                        principalTable: "Produit",
                        principalColumn: "IdProduit",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockProduit",
                columns: table => new
                {
                    IdStockProduit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QteInitialStockUnite = table.Column<int>(type: "int", nullable: false),
                    QteInitialStockKilo = table.Column<int>(type: "int", nullable: false),
                    QteStockUnite = table.Column<int>(type: "int", nullable: false),
                    QteStockKilo = table.Column<int>(type: "int", nullable: false),
                    DLC = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LotParDLC = table.Column<int>(type: "int", nullable: false),
                    DateMajStock = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdProduit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockProduit", x => x.IdStockProduit);
                    table.ForeignKey(
                        name: "FK_StockProduit_Produit_IdProduit",
                        column: x => x.IdProduit,
                        principalTable: "Produit",
                        principalColumn: "IdProduit",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProduitBCI",
                columns: table => new
                {
                    IdProduitBCI = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QteCommandeUnite = table.Column<int>(type: "int", nullable: false),
                    QteCommandeKilo = table.Column<int>(type: "int", nullable: false),
                    IdProduit = table.Column<int>(type: "int", nullable: false),
                    IdBCI = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitBCI", x => x.IdProduitBCI);
                    table.ForeignKey(
                        name: "FK_ProduitBCI_BCI_IdBCI",
                        column: x => x.IdBCI,
                        principalTable: "BCI",
                        principalColumn: "IdBCI",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduitBCI_Produit_IdProduit",
                        column: x => x.IdProduit,
                        principalTable: "Produit",
                        principalColumn: "IdProduit",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facture",
                columns: table => new
                {
                    IdFacture = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroFacture = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateFacture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemiseFacture = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    IdPersonnel = table.Column<int>(type: "int", nullable: false),
                    IdTicket = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facture", x => x.IdFacture);
                    table.ForeignKey(
                        name: "FK_Facture_Personnel_IdPersonnel",
                        column: x => x.IdPersonnel,
                        principalTable: "Personnel",
                        principalColumn: "IdPersonnel",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Facture_Ticket_IdTicket",
                        column: x => x.IdTicket,
                        principalTable: "Ticket",
                        principalColumn: "IdTicket",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "TicketProduit",
                columns: table => new
                {
                    IdTicketProduit = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QteCommandeeUnitaire = table.Column<int>(type: "int", nullable: false),
                    QteCommandeeKilo = table.Column<int>(type: "int", nullable: false),
                    QteLivreeRecueUnitaire = table.Column<int>(type: "int", nullable: false),
                    QteLivreeRecueKilo = table.Column<int>(type: "int", nullable: false),
                    IdTicket = table.Column<int>(type: "int", nullable: false),
                    IdProduit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketProduit", x => x.IdTicketProduit);
                    table.ForeignKey(
                        name: "FK_TicketProduit_Produit_IdProduit",
                        column: x => x.IdProduit,
                        principalTable: "Produit",
                        principalColumn: "IdProduit",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketProduit_Ticket_IdTicket",
                        column: x => x.IdTicket,
                        principalTable: "Ticket",
                        principalColumn: "IdTicket",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProduitCommandeFournisseur",
                columns: table => new
                {
                    IdProduitCommandeFournisseur = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QtePdtCmdeFsseurUnite = table.Column<int>(type: "int", nullable: false),
                    QtePdtCmdeFsseurKilo = table.Column<int>(type: "int", nullable: false),
                    IdCommandeFournisseur = table.Column<int>(type: "int", nullable: false),
                    IdProduit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProduitCommandeFournisseur", x => x.IdProduitCommandeFournisseur);
                    table.ForeignKey(
                        name: "FK_ProduitCommandeFournisseur_CommandeFournisseur_IdCommandeFournisseur",
                        column: x => x.IdCommandeFournisseur,
                        principalTable: "CommandeFournisseur",
                        principalColumn: "IdCommandeFournisseur",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProduitCommandeFournisseur_Produit_IdProduit",
                        column: x => x.IdProduit,
                        principalTable: "Produit",
                        principalColumn: "IdProduit",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BCI_IdPersonnel",
                table: "BCI",
                column: "IdPersonnel");

            migrationBuilder.CreateIndex(
                name: "IX_BCI_IdTypeBCI",
                table: "BCI",
                column: "IdTypeBCI");

            migrationBuilder.CreateIndex(
                name: "IX_Catalogue_IdTypeCatalogue",
                table: "Catalogue",
                column: "IdTypeCatalogue");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogueProduit_IdCatalogue",
                table: "CatalogueProduit",
                column: "IdCatalogue");

            migrationBuilder.CreateIndex(
                name: "IX_CatalogueProduit_IdProduit",
                table: "CatalogueProduit",
                column: "IdProduit");

            migrationBuilder.CreateIndex(
                name: "IX_CommandeFournisseur_IdFournisseur",
                table: "CommandeFournisseur",
                column: "IdFournisseur");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_IdPersonnel",
                table: "Facture",
                column: "IdPersonnel");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_IdTicket",
                table: "Facture",
                column: "IdTicket");

            migrationBuilder.CreateIndex(
                name: "IX_Fournisseur_IdSource",
                table: "Fournisseur",
                column: "IdSource");

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_IdFonction",
                table: "Personnel",
                column: "IdFonction");

            migrationBuilder.CreateIndex(
                name: "IX_Personnel_IdService",
                table: "Personnel",
                column: "IdService");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_IdFamille",
                table: "Produit",
                column: "IdFamille");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_IdSource",
                table: "Produit",
                column: "IdSource");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_IdSousFamille",
                table: "Produit",
                column: "IdSousFamille");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_IdUnite",
                table: "Produit",
                column: "IdUnite");

            migrationBuilder.CreateIndex(
                name: "IX_Produit_IdZoneStockage",
                table: "Produit",
                column: "IdZoneStockage");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitBCI_IdBCI",
                table: "ProduitBCI",
                column: "IdBCI");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitBCI_IdProduit",
                table: "ProduitBCI",
                column: "IdProduit");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitCommandeFournisseur_IdCommandeFournisseur",
                table: "ProduitCommandeFournisseur",
                column: "IdCommandeFournisseur");

            migrationBuilder.CreateIndex(
                name: "IX_ProduitCommandeFournisseur_IdProduit",
                table: "ProduitCommandeFournisseur",
                column: "IdProduit");

            migrationBuilder.CreateIndex(
                name: "IX_SousFamille_IdFamille",
                table: "SousFamille",
                column: "IdFamille");

            migrationBuilder.CreateIndex(
                name: "IX_StockProduit_IdProduit",
                table: "StockProduit",
                column: "IdProduit");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_IdPersonnel",
                table: "Ticket",
                column: "IdPersonnel");

            migrationBuilder.CreateIndex(
                name: "IX_Ticket_IdTypeTicket",
                table: "Ticket",
                column: "IdTypeTicket");

            migrationBuilder.CreateIndex(
                name: "IX_TicketProduit_IdProduit",
                table: "TicketProduit",
                column: "IdProduit");

            migrationBuilder.CreateIndex(
                name: "IX_TicketProduit_IdTicket",
                table: "TicketProduit",
                column: "IdTicket");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatalogueProduit");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Facture");

            migrationBuilder.DropTable(
                name: "Marque");

            migrationBuilder.DropTable(
                name: "Origine");

            migrationBuilder.DropTable(
                name: "ProduitBCI");

            migrationBuilder.DropTable(
                name: "ProduitCommandeFournisseur");

            migrationBuilder.DropTable(
                name: "StockProduit");

            migrationBuilder.DropTable(
                name: "SubstitutionProduit");

            migrationBuilder.DropTable(
                name: "TicketProduit");

            migrationBuilder.DropTable(
                name: "Catalogue");

            migrationBuilder.DropTable(
                name: "BCI");

            migrationBuilder.DropTable(
                name: "CommandeFournisseur");

            migrationBuilder.DropTable(
                name: "Produit");

            migrationBuilder.DropTable(
                name: "Ticket");

            migrationBuilder.DropTable(
                name: "TypeCatalogue");

            migrationBuilder.DropTable(
                name: "TypeBCI");

            migrationBuilder.DropTable(
                name: "Fournisseur");

            migrationBuilder.DropTable(
                name: "SousFamille");

            migrationBuilder.DropTable(
                name: "Unite");

            migrationBuilder.DropTable(
                name: "ZoneStockage");

            migrationBuilder.DropTable(
                name: "Personnel");

            migrationBuilder.DropTable(
                name: "TypeTicket");

            migrationBuilder.DropTable(
                name: "Source");

            migrationBuilder.DropTable(
                name: "Famille");

            migrationBuilder.DropTable(
                name: "Fonction");

            migrationBuilder.DropTable(
                name: "Service");
        }
    }
}
