using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class TicketRepository : RepositoryBase<Ticket>,ITicketRepository
    {	
		public TicketRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<TicketView> TIC()
        {
            return from t in AppDBContext.Tickets
                select new TicketView()
                {   
                    IdTicket = t.IdTicket,
                    DateCommande = t.DateCommande,
                    DateValidation = t.DateValidation,
                    DateLivraison = t.DateLivraison,
                    ObservationTicket = t.ObservationTicket,
                    IsTicketValider = t.IsTicketValider,
                    IdService = t.IdService,
                    NomService = AppDBContext.Services.FirstOrDefault(f=>f.IdService==t.IdService).NomService,
                    IdClient = t.IdClient,
                    NomClient = AppDBContext.Clients.FirstOrDefault(f=>f.IdClient==t.IdClient).NomClient,
                    IdFournisseur = t.IdFournisseur,
                    NomFournisseur = AppDBContext.Fournisseurs.FirstOrDefault(f=>f.IdFournisseur==t.IdFournisseur).NomFournisseur,
                    IdPersonnel = t.IdPersonnel,
                    NomPersonnel = t.Personnel.NomPersonnel,
                    PrenomPersonnel = t.Personnel.PrenomPersonnel,
                    TelPersonnel = t.Personnel.TelPersonnel,
                    IdValideur = t.IdValideur,
                    IdTypeTicket = t.IdTypeTicket,
                    NomTypeTicket = t.TypeTicket.NomTypeTicket,
                    
                };
        }
        
        public TicketView GetListAllTickets(int idTicket)
        {
           return TIC().FirstOrDefault(c=>c.IdTicket ==idTicket);          
        }

        public IEnumerable<TicketView> GetListAllTickets()
        {
            return TIC().ToList();
        }
    }
}
	
