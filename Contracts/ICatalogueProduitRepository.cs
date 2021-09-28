using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface ICatalogueProduitRepository : IRepositoryBase<CatalogueProduit>
    {
        IEnumerable<CatalogueProduitView> GetListAllCatalogueProduits();
        CatalogueProduitView GetListAllCatalogueProduits(int idCatalogueProduit);
    }
}
	
