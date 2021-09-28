using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IFournisseurRepository : IRepositoryBase<Fournisseur>
    {
        IEnumerable<FournisseurView> GetListAllFournisseurs();
        FournisseurView GetListAllFournisseurs(int idFournisseur);
    }
}
	
