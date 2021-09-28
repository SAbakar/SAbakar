using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface ICatalogueRepository : IRepositoryBase<Catalogue>
    {
        IEnumerable<CatalogueView> GetListAllCatalogues();
        CatalogueView GetListAllCatalogues(int idCatalogue);
    }
}
	
