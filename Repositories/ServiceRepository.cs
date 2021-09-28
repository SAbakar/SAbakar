using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class ServiceRepository : RepositoryBase<Service>,IServiceRepository
    {	
		public ServiceRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<ServiceView> SER()
        {
            return from s in AppDBContext.Services
                select new ServiceView()
                {   
                    IdService = s.IdService,
                    NomService = s.NomService,
                    
                };
        }
        
        public ServiceView GetListAllServices(int idService)
        {
           return SER().FirstOrDefault(c=>c.IdService ==idService);          
        }

        public IEnumerable<ServiceView> GetListAllServices()
        {
            return SER().ToList();
        }
    }
}
	
