using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IFonctionRepository : IRepositoryBase<Fonction>
    {
        IEnumerable<FonctionView> GetListAllFonctions();
        FonctionView GetListAllFonctions(int idFonction);
    }
}
	
