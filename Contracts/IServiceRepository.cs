using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IServiceRepository : IRepositoryBase<Service>
    {
        IEnumerable<ServiceView> GetListAllServices();
        ServiceView GetListAllServices(int idService);
    }
}
	
