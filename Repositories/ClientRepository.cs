
using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class ClientRepository : RepositoryBase<Client>,IClientRepository
    {	
		public ClientRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<ClientView> CLI()
        {
            return from c in AppDBContext.Clients
                select new ClientView()
                {   
                    IdClient = c.IdClient,
                    NomClient = c.NomClient,
                    
                };
        }
        
        public ClientView GetListAllClients(int idClient)
        {
           return CLI().FirstOrDefault(c=>c.IdClient ==idClient);          
        }

        public IEnumerable<ClientView> GetListAllClients()
        {
            return CLI().ToList();
        }
    }
}
	
