using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface ICommandeFournisseurRepository : IRepositoryBase<CommandeFournisseur>
    {
        IEnumerable<CommandeFournisseurView> GetListAllCommandeFournisseurs();
        CommandeFournisseurView GetListAllCommandeFournisseurs(int idCommandeFournisseur);
    }
}
	
