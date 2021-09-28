using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class TypeCatalogueRepository : RepositoryBase<TypeCatalogue>,ITypeCatalogueRepository
    {	
		public TypeCatalogueRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<TypeCatalogueView> TYP()
        {
            return from t in AppDBContext.TypeCatalogues
                select new TypeCatalogueView()
                {   
                    IdTypeCatalogue = t.IdTypeCatalogue,
                    NomTypeCatalogue = t.NomTypeCatalogue,
                    
                };
        }
        
        public TypeCatalogueView GetListAllTypeCatalogues(int idTypeCatalogue)
        {
           return TYP().FirstOrDefault(c=>c.IdTypeCatalogue ==idTypeCatalogue);          
        }

        public IEnumerable<TypeCatalogueView> GetListAllTypeCatalogues()
        {
            return TYP().ToList();
        }
    }
}
	
