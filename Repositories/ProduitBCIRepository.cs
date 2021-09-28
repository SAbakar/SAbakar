using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class ProduitBCIRepository : RepositoryBase<ProduitBCI>,IProduitBCIRepository
    {	
		public ProduitBCIRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<ProduitBCIView> PRO()
        {
            return from p in AppDBContext.ProduitBCIs
                select new ProduitBCIView()
                {   
                    IdProduitBCI = p.IdProduitBCI,
                    QteCommandeUnite = p.QteCommandeUnite,
                    QteCommandeKilo = p.QteCommandeKilo,
                    IdProduit = p.IdProduit,
                    NomProduit = p.Produit.NomProduit,
                    CodeEAN = p.Produit.CodeEAN,
                    PrixAchat = p.Produit.PrixAchat,
                    PrixVente = p.Produit.PrixVente,
                    PoidsTotal = p.Produit.PoidsTotal,
                    StockMinimalUnite = p.Produit.StockMinimalUnite,
                    StockMinimalPoids = p.Produit.StockMinimalPoids,
                    QteReassort = p.Produit.QteReassort,
                    IsEnKilogramme = p.Produit.IsEnKilogramme,
                    UVC = p.Produit.UVC,
                    ShortTime = p.Produit.ShortTime,
                    ImageProduit = p.Produit.ImageProduit,
                    IdBCI = p.IdBCI,
                    NumeroBCI = p.BCI.NumeroBCI,
                    DateBCI = p.BCI.DateBCI,
                    DateValidationBCI = p.BCI.DateValidationBCI,
                    IsValider = p.BCI.IsValider,
                    IsAnnuler = p.BCI.IsAnnuler,
                    ObsBCI = p.BCI.ObsBCI,
                    
                };
        }
        
        public ProduitBCIView GetListAllProduitBCIs(int idProduitBCI)
        {
           return PRO().FirstOrDefault(c=>c.IdProduitBCI ==idProduitBCI);          
        }

        public IEnumerable<ProduitBCIView> GetListAllProduitBCIs()
        {
            return PRO().ToList();
        }
    }
}
	
