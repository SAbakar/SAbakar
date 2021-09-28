using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface ISubstitutionProduitRepository : IRepositoryBase<SubstitutionProduit>
    {
        IEnumerable<SubstitutionProduitView> GetListAllSubstitutionProduits();
        SubstitutionProduitView GetListAllSubstitutionProduits(int idSubstitutionProduit);
    }
}
	
