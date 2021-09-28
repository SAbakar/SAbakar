

using Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace Entities
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        public AppDBContext() { }

        public string CurrentUserId { get; set; }

        public DbSet<BCI> BCIs { get; set; }
        public DbSet<Catalogue> Catalogues { get; set; }
        public DbSet<CatalogueProduit> CatalogueProduits { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<CommandeFournisseur> CommandeFournisseurs { get; set; }
        public DbSet<Facture> Factures { get; set; }
        public DbSet<Famille> Familles { get; set; }
        public DbSet<Fonction> Fonctions { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }
        public DbSet<Marque> Marques { get; set; }
        public DbSet<Origine> Origines { get; set; }
        public DbSet<Personnel> Personnels { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<ProduitBCI> ProduitBCIs { get; set; }
        public DbSet<ProduitCommandeFournisseur> ProduitCommandeFournisseurs { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Source> Sources { get; set; }
        public DbSet<SousFamille> SousFamilles { get; set; }
        public DbSet<StockProduit> StockProduits { get; set; }
        public DbSet<SubstitutionProduit> SubstitutionProduits { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketProduit> TicketProduits { get; set; }
        public DbSet<TypeBCI> TypeBCIs { get; set; }
        public DbSet<TypeCatalogue> TypeCatalogues { get; set; }
        public DbSet<TypeTicket> TypeTickets { get; set; }
        public DbSet<Unite> Unites { get; set; }
        public DbSet<ZoneStockage> ZoneStockages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
