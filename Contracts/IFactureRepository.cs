using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IFactureRepository : IRepositoryBase<Facture>
    {
        IEnumerable<FactureView> GetListAllFactures();
        FactureView GetListAllFactures(int idFacture);
    }
}
	
