using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class ProduitCommandeFournisseurRepository : RepositoryBase<ProduitCommandeFournisseur>,IProduitCommandeFournisseurRepository
    {	
		public ProduitCommandeFournisseurRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<ProduitCommandeFournisseurView> PRO()
        {
            return from p in AppDBContext.ProduitCommandeFournisseurs
                select new ProduitCommandeFournisseurView()
                {   
                    IdProduitCommandeFournisseur = p.IdProduitCommandeFournisseur,
                    QtePdtCmdeFsseurUnite = p.QtePdtCmdeFsseurUnite,
                    QtePdtCmdeFsseurKilo = p.QtePdtCmdeFsseurKilo,
                    IdCommandeFournisseur = p.IdCommandeFournisseur,
                    NumeroCmdeFsseur = p.CommandeFournisseur.NumeroCmdeFsseur,
                    DateCmdeFsseur = p.CommandeFournisseur.DateCmdeFsseur,
                    ObsCmdeFsseur = p.CommandeFournisseur.ObsCmdeFsseur,
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
                    
                };
        }
        
        public ProduitCommandeFournisseurView GetListAllProduitCommandeFournisseurs(int idProduitCommandeFournisseur)
        {
           return PRO().FirstOrDefault(c=>c.IdProduitCommandeFournisseur ==idProduitCommandeFournisseur);          
        }

        public IEnumerable<ProduitCommandeFournisseurView> GetListAllProduitCommandeFournisseurs()
        {
            return PRO().ToList();
        }
    }
}
	
