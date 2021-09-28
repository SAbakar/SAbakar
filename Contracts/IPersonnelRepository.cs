using Entities.Models;
using Entities.Views;
using System.Collections.Generic;

namespace Contracts
{    
    public interface IPersonnelRepository : IRepositoryBase<Personnel>
    {
        IEnumerable<PersonnelView> GetListAllPersonnels();
        PersonnelView GetListAllPersonnels(int idPersonnel);
    }
}
	
