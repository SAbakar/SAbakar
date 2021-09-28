using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IProduitRepository : IRepositoryBase<Produit>
    {
        IEnumerable<ProduitView> GetListAllProduits();
        ProduitView GetListAllProduits(int idProduit);
    }
}
	
