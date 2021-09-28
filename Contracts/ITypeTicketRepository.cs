using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface ITypeTicketRepository : IRepositoryBase<TypeTicket>
    {
        IEnumerable<TypeTicketView> GetListAllTypeTickets();
        TypeTicketView GetListAllTypeTickets(int idTypeTicket);
    }
}
	
