using Contracts;
using Entities;
using Entities.Models;
using Entities.Views;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{    
    public class TypeBCIRepository : RepositoryBase<TypeBCI>,ITypeBCIRepository
    {	
		public TypeBCIRepository(AppDBContext appDBContext):base (appDBContext)
        {

        }

        private IEnumerable<TypeBCIView> TYP()
        {
            return from t in AppDBContext.TypeBCIs
                select new TypeBCIView()
                {   
                    IdTypeBCI = t.IdTypeBCI,
                    NomTypeBCI = t.NomTypeBCI,
                    
                };
        }
        
        public TypeBCIView GetListAllTypeBCIs(int idTypeBCI)
        {
           return TYP().FirstOrDefault(c=>c.IdTypeBCI ==idTypeBCI);          
        }

        public IEnumerable<TypeBCIView> GetListAllTypeBCIs()
        {
            return TYP().ToList();
        }
    }
}
	
