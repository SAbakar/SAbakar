using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface ISousFamilleRepository : IRepositoryBase<SousFamille>
    {
        IEnumerable<SousFamilleView> GetListAllSousFamilles();
        SousFamilleView GetListAllSousFamilles(int idSousFamille);
    }
}
	
