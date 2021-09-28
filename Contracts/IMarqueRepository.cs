using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IMarqueRepository : IRepositoryBase<Marque>
    {
        IEnumerable<MarqueView> GetListAllMarques();
        MarqueView GetListAllMarques(int idMarque);
    }
}
	
