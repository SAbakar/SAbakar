using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface ITicketRepository : IRepositoryBase<Ticket>
    {
        IEnumerable<TicketView> GetListAllTickets();
        TicketView GetListAllTickets(int idTicket);
    }
}
	
