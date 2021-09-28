using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class StockProduitRepository : RepositoryBase<StockProduit>,IStockProduitRepository
    {	
		public StockProduitRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<StockProduitView> STO()
        {
            return from s in AppDBContext.StockProduits
                select new StockProduitView()
                {   
                    IdStockProduit = s.IdStockProduit,
                    QteInitialStockUnite = s.QteInitialStockUnite,
                    QteInitialStockKilo = s.QteInitialStockKilo,
                    QteStockUnite = s.QteStockUnite,
                    QteStockKilo = s.QteStockKilo,
                    DLC = s.DLC,
                    LotParDLC = s.LotParDLC,
                    DateMajStock = s.DateMajStock,
                    IdProduit = s.IdProduit,
                    NomProduit = s.Produit.NomProduit,
                    CodeEAN = s.Produit.CodeEAN,
                    PrixAchat = s.Produit.PrixAchat,
                    PrixVente = s.Produit.PrixVente,
                    PoidsTotal = s.Produit.PoidsTotal,
                    StockMinimalUnite = s.Produit.StockMinimalUnite,
                    StockMinimalPoids = s.Produit.StockMinimalPoids,
                    QteReassort = s.Produit.QteReassort,
                    IsEnKilogramme = s.Produit.IsEnKilogramme,
                    UVC = s.Produit.UVC,
                    ShortTime = s.Produit.ShortTime,
                    ImageProduit = s.Produit.ImageProduit,
                    
                };
        }
        
        public StockProduitView GetListAllStockProduits(int idStockProduit)
        {
           return STO().FirstOrDefault(c=>c.IdStockProduit ==idStockProduit);          
        }

        public IEnumerable<StockProduitView> GetListAllStockProduits()
        {
            return STO().ToList();
        }
    }
}
	
