using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class FactureRepository : RepositoryBase<Facture>,IFactureRepository
    {	
		public FactureRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<FactureView> FAC()
        {
            return from f in AppDBContext.Factures
                select new FactureView()
                {   
                    IdFacture = f.IdFacture,
                    NumeroFacture = f.NumeroFacture,
                    DateFacture = f.DateFacture,
                    RemiseFacture = f.RemiseFacture,
                    IdPersonnel = f.IdPersonnel,
                    NomPersonnel = f.Personnel.NomPersonnel,
                    PrenomPersonnel = f.Personnel.PrenomPersonnel,
                    TelPersonnel = f.Personnel.TelPersonnel,
                    IdTicket = f.IdTicket,
                    DateCommande = f.Ticket.DateCommande,
                    DateValidation = f.Ticket.DateValidation,
                    DateLivraison = f.Ticket.DateLivraison,
                    ObservationTicket = f.Ticket.ObservationTicket,
                    IsTicketValider = f.Ticket.IsTicketValider,
                    
                };
        }
        
        public FactureView GetListAllFactures(int idFacture)
        {
           return FAC().FirstOrDefault(c=>c.IdFacture ==idFacture);          
        }

        public IEnumerable<FactureView> GetListAllFactures()
        {
            return FAC().ToList();
        }
    }
}
	
