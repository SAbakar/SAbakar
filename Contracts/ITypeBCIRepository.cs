using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface ITypeBCIRepository : IRepositoryBase<TypeBCI>
    {
        IEnumerable<TypeBCIView> GetListAllTypeBCIs();
        TypeBCIView GetListAllTypeBCIs(int idTypeBCI);
    }
}
	
