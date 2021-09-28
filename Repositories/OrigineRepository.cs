using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class OrigineRepository : RepositoryBase<Origine>,IOrigineRepository
    {	
		public OrigineRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<OrigineView> ORI()
        {
            return from o in AppDBContext.Origines
                select new OrigineView()
                {   
                    IdOrigine = o.IdOrigine,
                    NomOrigine = o.NomOrigine,
                    
                };
        }
        
        public OrigineView GetListAllOrigines(int idOrigine)
        {
           return ORI().FirstOrDefault(c=>c.IdOrigine ==idOrigine);          
        }

        public IEnumerable<OrigineView> GetListAllOrigines()
        {
            return ORI().ToList();
        }
    }
}
	
