using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class UniteRepository : RepositoryBase<Unite>,IUniteRepository
    {	
		public UniteRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<UniteView> UNI()
        {
            return from u in AppDBContext.Unites
                select new UniteView()
                {   
                    IdUnite = u.IdUnite,
                    NomUnite = u.NomUnite,
                    
                };
        }
        
        public UniteView GetListAllUnites(int idUnite)
        {
           return UNI().FirstOrDefault(c=>c.IdUnite ==idUnite);          
        }

        public IEnumerable<UniteView> GetListAllUnites()
        {
            return UNI().ToList();
        }
    }
}
	
