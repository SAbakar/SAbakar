using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IUniteRepository : IRepositoryBase<Unite>
    {
        IEnumerable<UniteView> GetListAllUnites();
        UniteView GetListAllUnites(int idUnite);
    }
}
	
