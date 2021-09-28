using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IClientRepository : IRepositoryBase<Client> 
    {
        IEnumerable<ClientView> GetListAllClients();
        ClientView GetListAllClients(int idClient);
    }
}
	
