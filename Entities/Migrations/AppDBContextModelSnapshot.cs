﻿// <auto-generated />
using System;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Entities.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Models.BCI", b =>
                {
                    b.Property<int>("IdBCI")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateBCI")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateValidationBCI")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdPersonnel")
                        .HasColumnType("int");

                    b.Property<int>("IdTypeBCI")
                        .HasColumnType("int");

                    b.Property<bool>("IsAnnuler")
                        .HasColumnType("bit");

                    b.Property<bool>("IsValider")
                        .HasColumnType("bit");

                    b.Property<string>("NumeroBCI")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObsBCI")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdBCI");

                    b.HasIndex("IdPersonnel");

                    b.HasIndex("IdTypeBCI");

                    b.ToTable("BCI");
                });

            modelBuilder.Entity("Entities.Models.Catalogue", b =>
                {
                    b.Property<int>("IdCatalogue")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdClientService")
                        .HasColumnType("int");

                    b.Property<int>("IdTypeCatalogue")
                        .HasColumnType("int");

                    b.Property<string>("NomCatalogue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCatalogue");

                    b.HasIndex("IdTypeCatalogue");

                    b.ToTable("Catalogue");
                });

            modelBuilder.Entity("Entities.Models.CatalogueProduit", b =>
                {
                    b.Property<int>("IdCatalogueProduit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdCatalogue")
                        .HasColumnType("int");

                    b.Property<int>("IdProduit")
                        .HasColumnType("int");

                    b.HasKey("IdCatalogueProduit");

                    b.HasIndex("IdCatalogue");

                    b.HasIndex("IdProduit");

                    b.ToTable("CatalogueProduit");
                });

            modelBuilder.Entity("Entities.Models.Client", b =>
                {
                    b.Property<int>("IdClient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomClient")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdClient");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Entities.Models.CommandeFournisseur", b =>
                {
                    b.Property<int>("IdCommandeFournisseur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCmdeFsseur")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdFournisseur")
                        .HasColumnType("int");

                    b.Property<string>("NumeroCmdeFsseur")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObsCmdeFsseur")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCommandeFournisseur");

                    b.HasIndex("IdFournisseur");

                    b.ToTable("CommandeFournisseur");
                });

            modelBuilder.Entity("Entities.Models.Facture", b =>
                {
                    b.Property<int>("IdFacture")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateFacture")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdPersonnel")
                        .HasColumnType("int");

                    b.Property<int>("IdTicket")
                        .HasColumnType("int");

                    b.Property<string>("NumeroFacture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("RemiseFacture")
                        .HasColumnType("decimal(18,0)");

                    b.HasKey("IdFacture");

                    b.HasIndex("IdPersonnel");

                    b.HasIndex("IdTicket");

                    b.ToTable("Facture");
                });

            modelBuilder.Entity("Entities.Models.Famille", b =>
                {
                    b.Property<int>("IdFamille")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomFamille")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdFamille");

                    b.ToTable("Famille");
                });

            modelBuilder.Entity("Entities.Models.Fonction", b =>
                {
                    b.Property<int>("IdFonction")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomFonction")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdFonction");

                    b.ToTable("Fonction");
                });

            modelBuilder.Entity("Entities.Models.Fournisseur", b =>
                {
                    b.Property<int>("IdFournisseur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdSource")
                        .HasColumnType("int");

                    b.Property<string>("NomFournisseur")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdFournisseur");

                    b.HasIndex("IdSource");

                    b.ToTable("Fournisseur");
                });

            modelBuilder.Entity("Entities.Models.Marque", b =>
                {
                    b.Property<int>("IdMarque")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomMarque")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdMarque");

                    b.ToTable("Marque");
                });

            modelBuilder.Entity("Entities.Models.Origine", b =>
                {
                    b.Property<int>("IdOrigine")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomOrigine")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdOrigine");

                    b.ToTable("Origine");
                });

            modelBuilder.Entity("Entities.Models.Personnel", b =>
                {
                    b.Property<int>("IdPersonnel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdFonction")
                        .HasColumnType("int");

                    b.Property<int>("IdService")
                        .HasColumnType("int");

                    b.Property<string>("NomPersonnel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrenomPersonnel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelPersonnel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPersonnel");

                    b.HasIndex("IdFonction");

                    b.HasIndex("IdService");

                    b.ToTable("Personnel");
                });

            modelBuilder.Entity("Entities.Models.Produit", b =>
                {
                    b.Property<int>("IdProduit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodeEAN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdFamille")
                        .HasColumnType("int");

                    b.Property<int>("IdMarque")
                        .HasColumnType("int");

                    b.Property<int>("IdOrigine")
                        .HasColumnType("int");

                    b.Property<int>("IdSource")
                        .HasColumnType("int");

                    b.Property<int>("IdSousFamille")
                        .HasColumnType("int");

                    b.Property<int>("IdUnite")
                        .HasColumnType("int");

                    b.Property<int>("IdUniteFacturation")
                        .HasColumnType("int");

                    b.Property<int>("IdUniteGestionStock")
                        .HasColumnType("int");

                    b.Property<int>("IdZoneStockage")
                        .HasColumnType("int");

                    b.Property<string>("ImageProduit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEnKilogramme")
                        .HasColumnType("bit");

                    b.Property<string>("NomProduit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PoidsTotal")
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal>("PrixAchat")
                        .HasColumnType("decimal(18,0)");

                    b.Property<decimal>("PrixVente")
                        .HasColumnType("decimal(18,0)");

                    b.Property<int>("QteReassort")
                        .HasColumnType("int");

                    b.Property<int>("ShortTime")
                        .HasColumnType("int");

                    b.Property<decimal>("StockMinimalPoids")
                        .HasColumnType("decimal(18,0)");

                    b.Property<int>("StockMinimalUnite")
                        .HasColumnType("int");

                    b.Property<int>("UVC")
                        .HasColumnType("int");

                    b.HasKey("IdProduit");

                    b.HasIndex("IdFamille");

                    b.HasIndex("IdSource");

                    b.HasIndex("IdSousFamille");

                    b.HasIndex("IdUnite");

                    b.HasIndex("IdZoneStockage");

                    b.ToTable("Produit");
                });

            modelBuilder.Entity("Entities.Models.ProduitBCI", b =>
                {
                    b.Property<int>("IdProduitBCI")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdBCI")
                        .HasColumnType("int");

                    b.Property<int>("IdProduit")
                        .HasColumnType("int");

                    b.Property<int>("QteCommandeKilo")
                        .HasColumnType("int");

                    b.Property<int>("QteCommandeUnite")
                        .HasColumnType("int");

                    b.HasKey("IdProduitBCI");

                    b.HasIndex("IdBCI");

                    b.HasIndex("IdProduit");

                    b.ToTable("ProduitBCI");
                });

            modelBuilder.Entity("Entities.Models.ProduitCommandeFournisseur", b =>
                {
                    b.Property<int>("IdProduitCommandeFournisseur")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdCommandeFournisseur")
                        .HasColumnType("int");

                    b.Property<int>("IdProduit")
                        .HasColumnType("int");

                    b.Property<int>("QtePdtCmdeFsseurKilo")
                        .HasColumnType("int");

                    b.Property<int>("QtePdtCmdeFsseurUnite")
                        .HasColumnType("int");

                    b.HasKey("IdProduitCommandeFournisseur");

                    b.HasIndex("IdCommandeFournisseur");

                    b.HasIndex("IdProduit");

                    b.ToTable("ProduitCommandeFournisseur");
                });

            modelBuilder.Entity("Entities.Models.Service", b =>
                {
                    b.Property<int>("IdService")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomService")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdService");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("Entities.Models.Source", b =>
                {
                    b.Property<int>("IdSource")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomSource")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSource");

                    b.ToTable("Source");
                });

            modelBuilder.Entity("Entities.Models.SousFamille", b =>
                {
                    b.Property<int>("IdSousFamille")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdFamille")
                        .HasColumnType("int");

                    b.Property<string>("NomSousFamille")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSousFamille");

                    b.HasIndex("IdFamille");

                    b.ToTable("SousFamille");
                });

            modelBuilder.Entity("Entities.Models.StockProduit", b =>
                {
                    b.Property<int>("IdStockProduit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DLC")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateMajStock")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdProduit")
                        .HasColumnType("int");

                    b.Property<int>("LotParDLC")
                        .HasColumnType("int");

                    b.Property<int>("QteInitialStockKilo")
                        .HasColumnType("int");

                    b.Property<int>("QteInitialStockUnite")
                        .HasColumnType("int");

                    b.Property<int>("QteStockKilo")
                        .HasColumnType("int");

                    b.Property<int>("QteStockUnite")
                        .HasColumnType("int");

                    b.HasKey("IdStockProduit");

                    b.HasIndex("IdProduit");

                    b.ToTable("StockProduit");
                });

            modelBuilder.Entity("Entities.Models.SubstitutionProduit", b =>
                {
                    b.Property<int>("IdSubstitutionProduit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdProduitPrincipal")
                        .HasColumnType("int");

                    b.Property<int>("IdProduitSubstitution")
                        .HasColumnType("int");

                    b.HasKey("IdSubstitutionProduit");

                    b.ToTable("SubstitutionProduit");
                });

            modelBuilder.Entity("Entities.Models.Ticket", b =>
                {
                    b.Property<int>("IdTicket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCommande")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateLivraison")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateValidation")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<int>("IdFournisseur")
                        .HasColumnType("int");

                    b.Property<int>("IdPersonnel")
                        .HasColumnType("int");

                    b.Property<int>("IdService")
                        .HasColumnType("int");

                    b.Property<int>("IdTypeTicket")
                        .HasColumnType("int");

                    b.Property<int>("IdValideur")
                        .HasColumnType("int");

                    b.Property<bool>("IsTicketValider")
                        .HasColumnType("bit");

                    b.Property<string>("ObservationTicket")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTicket");

                    b.HasIndex("IdPersonnel");

                    b.HasIndex("IdTypeTicket");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("Entities.Models.TicketProduit", b =>
                {
                    b.Property<int>("IdTicketProduit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("IdProduit")
                        .HasColumnType("int");

                    b.Property<int>("IdTicket")
                        .HasColumnType("int");

                    b.Property<int>("QteCommandeeKilo")
                        .HasColumnType("int");

                    b.Property<int>("QteCommandeeUnitaire")
                        .HasColumnType("int");

                    b.Property<int>("QteLivreeRecueKilo")
                        .HasColumnType("int");

                    b.Property<int>("QteLivreeRecueUnitaire")
                        .HasColumnType("int");

                    b.HasKey("IdTicketProduit");

                    b.HasIndex("IdProduit");

                    b.HasIndex("IdTicket");

                    b.ToTable("TicketProduit");
                });

            modelBuilder.Entity("Entities.Models.TypeBCI", b =>
                {
                    b.Property<int>("IdTypeBCI")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomTypeBCI")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTypeBCI");

                    b.ToTable("TypeBCI");
                });

            modelBuilder.Entity("Entities.Models.TypeCatalogue", b =>
                {
                    b.Property<int>("IdTypeCatalogue")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomTypeCatalogue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTypeCatalogue");

                    b.ToTable("TypeCatalogue");
                });

            modelBuilder.Entity("Entities.Models.TypeTicket", b =>
                {
                    b.Property<int>("IdTypeTicket")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomTypeTicket")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTypeTicket");

                    b.ToTable("TypeTicket");
                });

            modelBuilder.Entity("Entities.Models.Unite", b =>
                {
                    b.Property<int>("IdUnite")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomUnite")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUnite");

                    b.ToTable("Unite");
                });

            modelBuilder.Entity("Entities.Models.ZoneStockage", b =>
                {
                    b.Property<int>("IdZoneStockage")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NomZoneStockage")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdZoneStockage");

                    b.ToTable("ZoneStockage");
                });

            modelBuilder.Entity("Entities.Models.BCI", b =>
                {
                    b.HasOne("Entities.Models.Personnel", "Personnel")
                        .WithMany()
                        .HasForeignKey("IdPersonnel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.TypeBCI", "TypeBCI")
                        .WithMany()
                        .HasForeignKey("IdTypeBCI")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personnel");

                    b.Navigation("TypeBCI");
                });

            modelBuilder.Entity("Entities.Models.Catalogue", b =>
                {
                    b.HasOne("Entities.Models.TypeCatalogue", "TypeCatalogue")
                        .WithMany()
                        .HasForeignKey("IdTypeCatalogue")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TypeCatalogue");
                });

            modelBuilder.Entity("Entities.Models.CatalogueProduit", b =>
                {
                    b.HasOne("Entities.Models.Catalogue", "Catalogue")
                        .WithMany()
                        .HasForeignKey("IdCatalogue")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Produit", "Produit")
                        .WithMany()
                        .HasForeignKey("IdProduit")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Catalogue");

                    b.Navigation("Produit");
                });

            modelBuilder.Entity("Entities.Models.CommandeFournisseur", b =>
                {
                    b.HasOne("Entities.Models.Fournisseur", "Fournisseur")
                        .WithMany()
                        .HasForeignKey("IdFournisseur")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fournisseur");
                });

            modelBuilder.Entity("Entities.Models.Facture", b =>
                {
                    b.HasOne("Entities.Models.Personnel", "Personnel")
                        .WithMany()
                        .HasForeignKey("IdPersonnel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("IdTicket")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personnel");

                    b.Navigation("Ticket");
                });

            modelBuilder.Entity("Entities.Models.Fournisseur", b =>
                {
                    b.HasOne("Entities.Models.Source", "Source")
                        .WithMany()
                        .HasForeignKey("IdSource")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Source");
                });

            modelBuilder.Entity("Entities.Models.Personnel", b =>
                {
                    b.HasOne("Entities.Models.Fonction", "Fonction")
                        .WithMany()
                        .HasForeignKey("IdFonction")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("IdService")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Fonction");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Entities.Models.Produit", b =>
                {
                    b.HasOne("Entities.Models.Famille", "Famille")
                        .WithMany()
                        .HasForeignKey("IdFamille")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Source", "Source")
                        .WithMany()
                        .HasForeignKey("IdSource")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.SousFamille", "SousFamille")
                        .WithMany()
                        .HasForeignKey("IdSousFamille")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Unite", "Unite")
                        .WithMany()
                        .HasForeignKey("IdUnite")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.ZoneStockage", "ZoneStockage")
                        .WithMany()
                        .HasForeignKey("IdZoneStockage")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Famille");

                    b.Navigation("Source");

                    b.Navigation("SousFamille");

                    b.Navigation("Unite");

                    b.Navigation("ZoneStockage");
                });

            modelBuilder.Entity("Entities.Models.ProduitBCI", b =>
                {
                    b.HasOne("Entities.Models.BCI", "BCI")
                        .WithMany()
                        .HasForeignKey("IdBCI")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Produit", "Produit")
                        .WithMany()
                        .HasForeignKey("IdProduit")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BCI");

                    b.Navigation("Produit");
                });

            modelBuilder.Entity("Entities.Models.ProduitCommandeFournisseur", b =>
                {
                    b.HasOne("Entities.Models.CommandeFournisseur", "CommandeFournisseur")
                        .WithMany()
                        .HasForeignKey("IdCommandeFournisseur")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Produit", "Produit")
                        .WithMany()
                        .HasForeignKey("IdProduit")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CommandeFournisseur");

                    b.Navigation("Produit");
                });

            modelBuilder.Entity("Entities.Models.SousFamille", b =>
                {
                    b.HasOne("Entities.Models.Famille", "Famille")
                        .WithMany()
                        .HasForeignKey("IdFamille")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Famille");
                });

            modelBuilder.Entity("Entities.Models.StockProduit", b =>
                {
                    b.HasOne("Entities.Models.Produit", "Produit")
                        .WithMany()
                        .HasForeignKey("IdProduit")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produit");
                });

            modelBuilder.Entity("Entities.Models.Ticket", b =>
                {
                    b.HasOne("Entities.Models.Personnel", "Personnel")
                        .WithMany()
                        .HasForeignKey("IdPersonnel")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.TypeTicket", "TypeTicket")
                        .WithMany()
                        .HasForeignKey("IdTypeTicket")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Personnel");

                    b.Navigation("TypeTicket");
                });

            modelBuilder.Entity("Entities.Models.TicketProduit", b =>
                {
                    b.HasOne("Entities.Models.Produit", "Produit")
                        .WithMany()
                        .HasForeignKey("IdProduit")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Entities.Models.Ticket", "Ticket")
                        .WithMany()
                        .HasForeignKey("IdTicket")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Produit");

                    b.Navigation("Ticket");
                });
#pragma warning restore 612, 618
        }
    }
}