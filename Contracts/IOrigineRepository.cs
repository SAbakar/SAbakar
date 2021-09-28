using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IOrigineRepository : IRepositoryBase<Origine>
    {
        IEnumerable<OrigineView> GetListAllOrigines();
        OrigineView GetListAllOrigines(int idOrigine);
    }
}
	
