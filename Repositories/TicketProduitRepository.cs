using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class TicketProduitRepository : RepositoryBase<TicketProduit>,ITicketProduitRepository
    {	
		public TicketProduitRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<TicketProduitView> TIC()
        {
            return from t in AppDBContext.TicketProduits
                select new TicketProduitView()
                {   
                    IdTicketProduit = t.IdTicketProduit,
                    QteCommandeeUnitaire = t.QteCommandeeUnitaire,
                    QteCommandeeKilo = t.QteCommandeeKilo,
                    QteLivreeRecueUnitaire = t.QteLivreeRecueUnitaire,
                    QteLivreeRecueKilo = t.QteLivreeRecueKilo,
                    IdTicket = t.IdTicket,
                    DateCommande = t.Ticket.DateCommande,
                    DateValidation = t.Ticket.DateValidation,
                    DateLivraison = t.Ticket.DateLivraison,
                    ObservationTicket = t.Ticket.ObservationTicket,
                    IsTicketValider = t.Ticket.IsTicketValider,
                    IdProduit = t.IdProduit,
                    NomProduit = t.Produit.NomProduit,
                    CodeEAN = t.Produit.CodeEAN,
                    PrixAchat = t.Produit.PrixAchat,
                    PrixVente = t.Produit.PrixVente,
                    PoidsTotal = t.Produit.PoidsTotal,
                    StockMinimalUnite = t.Produit.StockMinimalUnite,
                    StockMinimalPoids = t.Produit.StockMinimalPoids,
                    QteReassort = t.Produit.QteReassort,
                    IsEnKilogramme = t.Produit.IsEnKilogramme,
                    UVC = t.Produit.UVC,
                    ShortTime = t.Produit.ShortTime,
                    ImageProduit = t.Produit.ImageProduit,
                    
                };
        }
        
        public TicketProduitView GetListAllTicketProduits(int idTicketProduit)
        {
           return TIC().FirstOrDefault(c=>c.IdTicketProduit ==idTicketProduit);          
        }

        public IEnumerable<TicketProduitView> GetListAllTicketProduits()
        {
            return TIC().ToList();
        }
    }
}
	
