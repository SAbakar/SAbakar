using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IProduitCommandeFournisseurRepository : IRepositoryBase<ProduitCommandeFournisseur>
    {
        IEnumerable<ProduitCommandeFournisseurView> GetListAllProduitCommandeFournisseurs();
        ProduitCommandeFournisseurView GetListAllProduitCommandeFournisseurs(int idProduitCommandeFournisseur);
    }
}
	
