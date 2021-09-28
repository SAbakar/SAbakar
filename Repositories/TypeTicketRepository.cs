using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class TypeTicketRepository : RepositoryBase<TypeTicket>,ITypeTicketRepository
    {	
		public TypeTicketRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<TypeTicketView> TYP()
        {
            return from t in AppDBContext.TypeTickets
                select new TypeTicketView()
                {   
                    IdTypeTicket = t.IdTypeTicket,
                    NomTypeTicket = t.NomTypeTicket,
                    
                };
        }
        
        public TypeTicketView GetListAllTypeTickets(int idTypeTicket)
        {
           return TYP().FirstOrDefault(c=>c.IdTypeTicket ==idTypeTicket);          
        }

        public IEnumerable<TypeTicketView> GetListAllTypeTickets()
        {
            return TYP().ToList();
        }
    }
}
	
