using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class ProduitRepository : RepositoryBase<Produit>,IProduitRepository
    {	
		public ProduitRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<ProduitView> PRO()
        {
            return from p in AppDBContext.Produits
                select new ProduitView()
                {   
                    IdProduit = p.IdProduit,
                    NomProduit = p.NomProduit,
                    CodeEAN = p.CodeEAN,
                    PrixAchat = p.PrixAchat,
                    PrixVente = p.PrixVente,
                    PoidsTotal = p.PoidsTotal,
                    StockMinimalUnite = p.StockMinimalUnite,
                    StockMinimalPoids = p.StockMinimalPoids,
                    QteReassort = p.QteReassort,
                    IsEnKilogramme = p.IsEnKilogramme,
                    UVC = p.UVC,
                    ShortTime = p.ShortTime,
                    ImageProduit = p.ImageProduit,
                    IdFamille = p.IdFamille,
                    NomFamille = p.Famille.NomFamille,
                    IdSousFamille = p.IdSousFamille,
                    NomSousFamille = p.SousFamille.NomSousFamille,
                    IdZoneStockage = p.IdZoneStockage,
                    NomZoneStockage = p.ZoneStockage.NomZoneStockage,
                    IdSource = p.IdSource,
                    NomSource = p.Source.NomSource,
                    IdOrigine = p.IdOrigine,
                    NomOrigine = AppDBContext.Origines.FirstOrDefault(f=>f.IdOrigine ==p.IdOrigine).NomOrigine,
                    IdMarque = p.IdMarque,
                    NomMarque = AppDBContext.Marques.FirstOrDefault(f=>f.IdMarque ==p.IdMarque).NomMarque,
                    IdUnite = p.IdUnite,
                    NomUnite = p.Unite.NomUnite,
                    IdUniteFacturation = p.IdUniteFacturation,
                    IdUniteGestionStock = p.IdUniteGestionStock,
                    
                };
        }
        
        public ProduitView GetListAllProduits(int idProduit)
        {
           return PRO().FirstOrDefault(c=>c.IdProduit ==idProduit);          
        }

        public IEnumerable<ProduitView> GetListAllProduits()
        {
            return PRO().ToList();
        }
    }
}
	
