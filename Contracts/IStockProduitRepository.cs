using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IStockProduitRepository : IRepositoryBase<StockProduit>
    {
        IEnumerable<StockProduitView> GetListAllStockProduits();
        StockProduitView GetListAllStockProduits(int idStockProduit);
    }
}
	
