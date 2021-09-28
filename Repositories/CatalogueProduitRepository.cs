using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class CatalogueProduitRepository : RepositoryBase<CatalogueProduit>,ICatalogueProduitRepository
    {	
		public CatalogueProduitRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<CatalogueProduitView> CAT()
        {
            return from c in AppDBContext.CatalogueProduits
                select new CatalogueProduitView()
                {   
                    IdCatalogueProduit = c.IdCatalogueProduit,
                    IdProduit = c.IdProduit,
                    NomProduit = c.Produit.NomProduit,
                    CodeEAN = c.Produit.CodeEAN,
                    PrixAchat = c.Produit.PrixAchat,
                    PrixVente = c.Produit.PrixVente,
                    PoidsTotal = c.Produit.PoidsTotal,
                    StockMinimalUnite = c.Produit.StockMinimalUnite,
                    StockMinimalPoids = c.Produit.StockMinimalPoids,
                    QteReassort = c.Produit.QteReassort,
                    IsEnKilogramme = c.Produit.IsEnKilogramme,
                    UVC = c.Produit.UVC,
                    ShortTime = c.Produit.ShortTime,
                    ImageProduit = c.Produit.ImageProduit,
                    IdCatalogue = c.IdCatalogue,
                    NomCatalogue = c.Catalogue.NomCatalogue,
                    
                };
        }
        
        public CatalogueProduitView GetListAllCatalogueProduits(int idCatalogueProduit)
        {
           return CAT().FirstOrDefault(c=>c.IdCatalogueProduit ==idCatalogueProduit);          
        }

        public IEnumerable<CatalogueProduitView> GetListAllCatalogueProduits()
        {
            return CAT().ToList();
        }
    }
}
	
