using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IFamilleRepository : IRepositoryBase<Famille>
    {
        IEnumerable<FamilleView> GetListAllFamilles();
        FamilleView GetListAllFamilles(int idFamille);
    }
}
	
