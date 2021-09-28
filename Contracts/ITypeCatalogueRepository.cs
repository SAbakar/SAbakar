using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface ITypeCatalogueRepository : IRepositoryBase<TypeCatalogue>
    {
        IEnumerable<TypeCatalogueView> GetListAllTypeCatalogues();
        TypeCatalogueView GetListAllTypeCatalogues(int idTypeCatalogue);
    }
}
	
