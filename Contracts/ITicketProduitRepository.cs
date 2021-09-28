using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface ITicketProduitRepository : IRepositoryBase<TicketProduit>
    {
        IEnumerable<TicketProduitView> GetListAllTicketProduits();
        TicketProduitView GetListAllTicketProduits(int idTicketProduit);
    }
}
	
