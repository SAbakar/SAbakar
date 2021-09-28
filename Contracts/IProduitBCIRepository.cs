using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IProduitBCIRepository : IRepositoryBase<ProduitBCI>
    {
        IEnumerable<ProduitBCIView> GetListAllProduitBCIs();
        ProduitBCIView GetListAllProduitBCIs(int idProduitBCI);
    }
}
	
