using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class MarqueRepository : RepositoryBase<Marque>,IMarqueRepository
    {	
		public MarqueRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<MarqueView> MAR()
        {
            return from m in AppDBContext.Marques
                select new MarqueView()
                {   
                    IdMarque = m.IdMarque,
                    NomMarque = m.NomMarque,
                    
                };
        }
        
        public MarqueView GetListAllMarques(int idMarque)
        {
           return MAR().FirstOrDefault(c=>c.IdMarque ==idMarque);          
        }

        public IEnumerable<MarqueView> GetListAllMarques()
        {
            return MAR().ToList();
        }
    }
}
	
