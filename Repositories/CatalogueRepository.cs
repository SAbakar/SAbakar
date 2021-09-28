using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class CatalogueRepository : RepositoryBase<Catalogue>,ICatalogueRepository
    {	
		public CatalogueRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<CatalogueView> CAT()
        {
            return from c in AppDBContext.Catalogues
                select new CatalogueView()
                {   
                    IdCatalogue = c.IdCatalogue,
                    NomCatalogue = c.NomCatalogue,
                    IdClientService = c.IdClientService,
                    IdTypeCatalogue = c.IdTypeCatalogue,
                    NomTypeCatalogue = c.TypeCatalogue.NomTypeCatalogue,
                    
                };
        }
        
        public CatalogueView GetListAllCatalogues(int idCatalogue)
        {
           return CAT().FirstOrDefault(c=>c.IdCatalogue ==idCatalogue);          
        }

        public IEnumerable<CatalogueView> GetListAllCatalogues()
        {
            return CAT().ToList();
        }
    }
}
	
