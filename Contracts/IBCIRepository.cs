using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IBCIRepository : IRepositoryBase<BCI>
    {
        IEnumerable<BCIView> GetListAllBCIs();
        BCIView GetListAllBCIs(int idBCI);
    }
}
	
